using System;
using System.IO;
using System.Xml;
namespace ysl_template.Models
{
	public class FileItem
	{
		private string _configpath;
		private XmlDocument _doc;
		private string _filepath;
		private string _fileid;
		private DateTime _filetime = DateTime.MinValue;
		private string _filename;
		private string _filedesc;
		public string FilePath
		{
			get
			{
				return this._filepath;
			}
		}
		public string FileID
		{
			get
			{
				if (this._fileid == null)
				{
					string fileName = Path.GetFileName(this._filepath);
					this._fileid = fileName.Split(new char[]
					{
						'.'
					})[1];
				}
				return this._fileid;
			}
		}
		public DateTime UploadTime
		{
			get
			{
				if (this._filetime == DateTime.MinValue)
				{
					string fileName = Path.GetFileName(this._filepath);
					this._filetime = DateTime.ParseExact(fileName.Split(new char[]
					{
						'.'
					})[0], "yyyy-MM-dd HH-mm-ss", null);
				}
				return this._filetime;
			}
		}
		public int FileSize
		{
			get
			{
				return (int)new FileInfo(this._filepath).Length;
			}
		}
		public string FileName
		{
			get
			{
				this.LoadConfig();
				return this._filename;
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					throw new ArgumentNullException("value");
				}
				this.LoadConfig();
				this._filename = value;
				this._doc.DocumentElement.SetAttribute("name", value);
				this._doc.Save(this._configpath);
			}
		}
		public string Description
		{
			get
			{
				this.LoadConfig();
				return this._filedesc;
			}
			set
			{
				this.LoadConfig();
				this._filedesc = value;
				if (value != null)
				{
					this._doc.DocumentElement.SetAttribute("comment", value);
				}
				else
				{
					this._doc.DocumentElement.RemoveAttribute("comment");
				}
				this._doc.Save(this._configpath);
			}
		}
		internal FileItem(string filepath)
		{
			this._filepath = filepath;
		}
		internal FileItem(string filepath, string configpath, XmlDocument doc)
		{
			this._filepath = filepath;
			this._configpath = configpath;
			this._doc = doc;
		}
		private void LoadConfig()
		{
			if (this._doc != null)
			{
				return;
			}
			this._configpath = this._filepath.Remove(this._filepath.Length - 5) + ".config";
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(this._configpath);
			this._filename = xmlDocument.DocumentElement.GetAttribute("name");
			this._filedesc = xmlDocument.DocumentElement.GetAttribute("comment");
			this._doc = xmlDocument;
		}
		public void Delete()
		{
			this.LoadConfig();
			File.Delete(this._filepath);
			File.Delete(this._configpath);
		}
	}
}
