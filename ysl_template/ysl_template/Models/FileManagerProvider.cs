using System;
using System.IO;
using System.Text;
using System.Web;
using System.Xml;
namespace ysl_template.Models
{
	public class FileManagerProvider
	{
		private HttpContext _context;
		public FileManagerProvider()
		{
			this._context = HttpContext.Current;
		}
		protected string GetUserDirectory(string username, bool autocreate)
		{
			if (string.IsNullOrEmpty(username))
			{
				throw new ArgumentNullException("username");
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < username.Length; i++)
			{
				char c = username[i];
				if (char.IsLetterOrDigit(c))
				{
					stringBuilder.Append(c);
				}
				else
				{
					stringBuilder.Append("_").Append((int)c);
				}
			}
			string path = this._context.Response.ApplyAppPathModifier("~/FileManagerFolder/" + stringBuilder.ToString());
			string text = this._context.Server.MapPath(path);
			if (autocreate && !Directory.Exists(text))
			{
				Directory.CreateDirectory(text);
			}
			return text;
		}
		public FileItem[] GetFiles(string username)
		{
			string userDirectory = this.GetUserDirectory(username, false);
			if (!Directory.Exists(userDirectory))
			{
				return new FileItem[0];
			}
			string[] files = Directory.GetFiles(userDirectory, "*.resx");
			Array.Sort<string>(files);
			FileItem[] array = new FileItem[files.Length];
			for (int i = 0; i < files.Length; i++)
			{
				array[i] = new FileItem(files[i]);
			}
			return array;
		}
		public FileItem GetFileByID(string username, string fileid)
		{
			Guid guid = new Guid(fileid);
			string userDirectory = this.GetUserDirectory(username, false);
			if (!Directory.Exists(userDirectory))
			{
				return null;
			}
			string[] files = Directory.GetFiles(userDirectory, "*." + guid.ToString() + ".resx");
			if (files.Length == 0)
			{
				return null;
			}
			return new FileItem(files[0]);
		}
		public FileItem MoveFile(string username, string srcpath, string filename, string description)
		{
			if (string.IsNullOrEmpty("srcpath"))
			{
				throw new ArgumentNullException("srcpath");
			}
			if (string.IsNullOrEmpty("filename"))
			{
				throw new ArgumentNullException("filename");
			}
			if (!File.Exists(srcpath))
			{
				throw new Exception("srcpath not exists!");
			}
			string userDirectory = this.GetUserDirectory(username, true);
			string str = DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss") + "." + Guid.NewGuid().ToString();
			string text = Path.Combine(userDirectory, str + ".resx");
			string filename2 = Path.Combine(userDirectory, str + ".config");
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml("<file/>");
			xmlDocument.DocumentElement.SetAttribute("name", filename);
			if (description != null)
			{
				xmlDocument.DocumentElement.SetAttribute("comment", description);
			}
			File.Move(srcpath, text);
			xmlDocument.Save(filename2);
			return new FileItem(text);
		}
	}
}
