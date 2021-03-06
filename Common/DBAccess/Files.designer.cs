﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Common.DBAccess
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="IM")]
	public partial class FilesDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertFileItem(FileItem instance);
    partial void UpdateFileItem(FileItem instance);
    partial void DeleteFileItem(FileItem instance);
    #endregion
		
		public FilesDataContext() : 
				base(global::Common.Properties.Settings.Default.IMConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public FilesDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public FilesDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public FilesDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public FilesDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<FileItem> FileItems
		{
			get
			{
				return this.GetTable<FileItem>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.tblFiles")]
	public partial class FileItem : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private long _FileItemID;
		
		private string _FileID;
		
		private string _FileName;
		
		private System.DateTime _UploadDate;
		
		private System.Data.Linq.Binary _FileBytes;
		
		private string _FileHash;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnFileItemIDChanging(long value);
    partial void OnFileItemIDChanged();
    partial void OnFileIDChanging(string value);
    partial void OnFileIDChanged();
    partial void OnFileNameChanging(string value);
    partial void OnFileNameChanged();
    partial void OnUploadDateChanging(System.DateTime value);
    partial void OnUploadDateChanged();
    partial void OnFileBytesChanging(System.Data.Linq.Binary value);
    partial void OnFileBytesChanged();
    partial void OnFileHashChanging(string value);
    partial void OnFileHashChanged();
    #endregion
		
		public FileItem()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FileItemID", AutoSync=AutoSync.OnInsert, DbType="BigInt NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public long FileItemID
		{
			get
			{
				return this._FileItemID;
			}
			set
			{
				if ((this._FileItemID != value))
				{
					this.OnFileItemIDChanging(value);
					this.SendPropertyChanging();
					this._FileItemID = value;
					this.SendPropertyChanged("FileItemID");
					this.OnFileItemIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FileID", DbType="VarChar(36) NOT NULL", CanBeNull=false)]
		public string FileID
		{
			get
			{
				return this._FileID;
			}
			set
			{
				if ((this._FileID != value))
				{
					this.OnFileIDChanging(value);
					this.SendPropertyChanging();
					this._FileID = value;
					this.SendPropertyChanged("FileID");
					this.OnFileIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FileName", DbType="VarChar(255) NOT NULL", CanBeNull=false)]
		public string FileName
		{
			get
			{
				return this._FileName;
			}
			set
			{
				if ((this._FileName != value))
				{
					this.OnFileNameChanging(value);
					this.SendPropertyChanging();
					this._FileName = value;
					this.SendPropertyChanged("FileName");
					this.OnFileNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UploadDate", DbType="DateTime NOT NULL")]
		public System.DateTime UploadDate
		{
			get
			{
				return this._UploadDate;
			}
			set
			{
				if ((this._UploadDate != value))
				{
					this.OnUploadDateChanging(value);
					this.SendPropertyChanging();
					this._UploadDate = value;
					this.SendPropertyChanged("UploadDate");
					this.OnUploadDateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FileBytes", DbType="Image NOT NULL", CanBeNull=false, UpdateCheck=UpdateCheck.Never)]
		public System.Data.Linq.Binary FileBytes
		{
			get
			{
				return this._FileBytes;
			}
			set
			{
				if ((this._FileBytes != value))
				{
					this.OnFileBytesChanging(value);
					this.SendPropertyChanging();
					this._FileBytes = value;
					this.SendPropertyChanged("FileBytes");
					this.OnFileBytesChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FileHash", DbType="VarChar(255) NOT NULL", CanBeNull=false)]
		public string FileHash
		{
			get
			{
				return this._FileHash;
			}
			set
			{
				if ((this._FileHash != value))
				{
					this.OnFileHashChanging(value);
					this.SendPropertyChanging();
					this._FileHash = value;
					this.SendPropertyChanged("FileHash");
					this.OnFileHashChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
#pragma warning restore 1591
