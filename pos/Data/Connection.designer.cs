﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace pos.Data
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
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="POS_DB")]
	public partial class ConnectionDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertUSERS_TB(USERS_TB instance);
    partial void UpdateUSERS_TB(USERS_TB instance);
    partial void DeleteUSERS_TB(USERS_TB instance);
    partial void InsertBRAND_TB(BRAND_TB instance);
    partial void UpdateBRAND_TB(BRAND_TB instance);
    partial void DeleteBRAND_TB(BRAND_TB instance);
    partial void InsertCATEGORY_TB(CATEGORY_TB instance);
    partial void UpdateCATEGORY_TB(CATEGORY_TB instance);
    partial void DeleteCATEGORY_TB(CATEGORY_TB instance);
    partial void InsertPRODUCT_TB(PRODUCT_TB instance);
    partial void UpdatePRODUCT_TB(PRODUCT_TB instance);
    partial void DeletePRODUCT_TB(PRODUCT_TB instance);
    #endregion
		
		public ConnectionDataContext() : 
				base(global::pos.Properties.Settings.Default.POS_DBConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public ConnectionDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public ConnectionDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public ConnectionDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public ConnectionDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<USERS_TB> USERS_TBs
		{
			get
			{
				return this.GetTable<USERS_TB>();
			}
		}
		
		public System.Data.Linq.Table<BRAND_TB> BRAND_TBs
		{
			get
			{
				return this.GetTable<BRAND_TB>();
			}
		}
		
		public System.Data.Linq.Table<CATEGORY_TB> CATEGORY_TBs
		{
			get
			{
				return this.GetTable<CATEGORY_TB>();
			}
		}
		
		public System.Data.Linq.Table<PRODUCT_TB> PRODUCT_TBs
		{
			get
			{
				return this.GetTable<PRODUCT_TB>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.USERS_TB")]
	public partial class USERS_TB : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _ID;
		
		private string _NAME;
		
		private string _USERNAME;
		
		private string _PASSWORD;
		
		private string _CONTACT;
		
		private string _ROLE;
		
		private System.Nullable<bool> _STATUS;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIDChanging(int value);
    partial void OnIDChanged();
    partial void OnNAMEChanging(string value);
    partial void OnNAMEChanged();
    partial void OnUSERNAMEChanging(string value);
    partial void OnUSERNAMEChanged();
    partial void OnPASSWORDChanging(string value);
    partial void OnPASSWORDChanged();
    partial void OnCONTACTChanging(string value);
    partial void OnCONTACTChanged();
    partial void OnROLEChanging(string value);
    partial void OnROLEChanged();
    partial void OnSTATUSChanging(System.Nullable<bool> value);
    partial void OnSTATUSChanged();
    #endregion
		
		public USERS_TB()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				if ((this._ID != value))
				{
					this.OnIDChanging(value);
					this.SendPropertyChanging();
					this._ID = value;
					this.SendPropertyChanged("ID");
					this.OnIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_NAME", DbType="VarChar(50)")]
		public string NAME
		{
			get
			{
				return this._NAME;
			}
			set
			{
				if ((this._NAME != value))
				{
					this.OnNAMEChanging(value);
					this.SendPropertyChanging();
					this._NAME = value;
					this.SendPropertyChanged("NAME");
					this.OnNAMEChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_USERNAME", DbType="VarChar(50)")]
		public string USERNAME
		{
			get
			{
				return this._USERNAME;
			}
			set
			{
				if ((this._USERNAME != value))
				{
					this.OnUSERNAMEChanging(value);
					this.SendPropertyChanging();
					this._USERNAME = value;
					this.SendPropertyChanged("USERNAME");
					this.OnUSERNAMEChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PASSWORD", DbType="VarChar(50)")]
		public string PASSWORD
		{
			get
			{
				return this._PASSWORD;
			}
			set
			{
				if ((this._PASSWORD != value))
				{
					this.OnPASSWORDChanging(value);
					this.SendPropertyChanging();
					this._PASSWORD = value;
					this.SendPropertyChanged("PASSWORD");
					this.OnPASSWORDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CONTACT", DbType="VarChar(50)")]
		public string CONTACT
		{
			get
			{
				return this._CONTACT;
			}
			set
			{
				if ((this._CONTACT != value))
				{
					this.OnCONTACTChanging(value);
					this.SendPropertyChanging();
					this._CONTACT = value;
					this.SendPropertyChanged("CONTACT");
					this.OnCONTACTChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ROLE", DbType="VarChar(50)")]
		public string ROLE
		{
			get
			{
				return this._ROLE;
			}
			set
			{
				if ((this._ROLE != value))
				{
					this.OnROLEChanging(value);
					this.SendPropertyChanging();
					this._ROLE = value;
					this.SendPropertyChanged("ROLE");
					this.OnROLEChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_STATUS", DbType="Bit")]
		public System.Nullable<bool> STATUS
		{
			get
			{
				return this._STATUS;
			}
			set
			{
				if ((this._STATUS != value))
				{
					this.OnSTATUSChanging(value);
					this.SendPropertyChanging();
					this._STATUS = value;
					this.SendPropertyChanged("STATUS");
					this.OnSTATUSChanged();
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
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.BRAND_TB")]
	public partial class BRAND_TB : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _ID;
		
		private string _BRAND;
		
		private System.Nullable<bool> _STATUS;
		
		private EntitySet<PRODUCT_TB> _PRODUCT_TBs;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIDChanging(int value);
    partial void OnIDChanged();
    partial void OnBRANDChanging(string value);
    partial void OnBRANDChanged();
    partial void OnSTATUSChanging(System.Nullable<bool> value);
    partial void OnSTATUSChanged();
    #endregion
		
		public BRAND_TB()
		{
			this._PRODUCT_TBs = new EntitySet<PRODUCT_TB>(new Action<PRODUCT_TB>(this.attach_PRODUCT_TBs), new Action<PRODUCT_TB>(this.detach_PRODUCT_TBs));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				if ((this._ID != value))
				{
					this.OnIDChanging(value);
					this.SendPropertyChanging();
					this._ID = value;
					this.SendPropertyChanged("ID");
					this.OnIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_BRAND", DbType="VarChar(50)")]
		public string BRAND
		{
			get
			{
				return this._BRAND;
			}
			set
			{
				if ((this._BRAND != value))
				{
					this.OnBRANDChanging(value);
					this.SendPropertyChanging();
					this._BRAND = value;
					this.SendPropertyChanged("BRAND");
					this.OnBRANDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_STATUS", DbType="Bit")]
		public System.Nullable<bool> STATUS
		{
			get
			{
				return this._STATUS;
			}
			set
			{
				if ((this._STATUS != value))
				{
					this.OnSTATUSChanging(value);
					this.SendPropertyChanging();
					this._STATUS = value;
					this.SendPropertyChanged("STATUS");
					this.OnSTATUSChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="BRAND_TB_PRODUCT_TB", Storage="_PRODUCT_TBs", ThisKey="ID", OtherKey="BRAND_ID")]
		public EntitySet<PRODUCT_TB> PRODUCT_TBs
		{
			get
			{
				return this._PRODUCT_TBs;
			}
			set
			{
				this._PRODUCT_TBs.Assign(value);
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
		
		private void attach_PRODUCT_TBs(PRODUCT_TB entity)
		{
			this.SendPropertyChanging();
			entity.BRAND_TB = this;
		}
		
		private void detach_PRODUCT_TBs(PRODUCT_TB entity)
		{
			this.SendPropertyChanging();
			entity.BRAND_TB = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.CATEGORY_TB")]
	public partial class CATEGORY_TB : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _ID;
		
		private string _CATEGORY;
		
		private System.Nullable<bool> _STATUS;
		
		private EntitySet<PRODUCT_TB> _PRODUCT_TBs;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIDChanging(int value);
    partial void OnIDChanged();
    partial void OnCATEGORYChanging(string value);
    partial void OnCATEGORYChanged();
    partial void OnSTATUSChanging(System.Nullable<bool> value);
    partial void OnSTATUSChanged();
    #endregion
		
		public CATEGORY_TB()
		{
			this._PRODUCT_TBs = new EntitySet<PRODUCT_TB>(new Action<PRODUCT_TB>(this.attach_PRODUCT_TBs), new Action<PRODUCT_TB>(this.detach_PRODUCT_TBs));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				if ((this._ID != value))
				{
					this.OnIDChanging(value);
					this.SendPropertyChanging();
					this._ID = value;
					this.SendPropertyChanged("ID");
					this.OnIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CATEGORY", DbType="VarChar(50)")]
		public string CATEGORY
		{
			get
			{
				return this._CATEGORY;
			}
			set
			{
				if ((this._CATEGORY != value))
				{
					this.OnCATEGORYChanging(value);
					this.SendPropertyChanging();
					this._CATEGORY = value;
					this.SendPropertyChanged("CATEGORY");
					this.OnCATEGORYChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_STATUS", DbType="Bit")]
		public System.Nullable<bool> STATUS
		{
			get
			{
				return this._STATUS;
			}
			set
			{
				if ((this._STATUS != value))
				{
					this.OnSTATUSChanging(value);
					this.SendPropertyChanging();
					this._STATUS = value;
					this.SendPropertyChanged("STATUS");
					this.OnSTATUSChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="CATEGORY_TB_PRODUCT_TB", Storage="_PRODUCT_TBs", ThisKey="ID", OtherKey="CATEGORY_ID")]
		public EntitySet<PRODUCT_TB> PRODUCT_TBs
		{
			get
			{
				return this._PRODUCT_TBs;
			}
			set
			{
				this._PRODUCT_TBs.Assign(value);
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
		
		private void attach_PRODUCT_TBs(PRODUCT_TB entity)
		{
			this.SendPropertyChanging();
			entity.CATEGORY_TB = this;
		}
		
		private void detach_PRODUCT_TBs(PRODUCT_TB entity)
		{
			this.SendPropertyChanging();
			entity.CATEGORY_TB = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.PRODUCT_TB")]
	public partial class PRODUCT_TB : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _ID;
		
		private System.Nullable<int> _BRAND_ID;
		
		private System.Nullable<int> _CATEGORY_ID;
		
		private string _PRODUCT;
		
		private System.Nullable<decimal> _SELL_PRICE;
		
		private System.Nullable<System.DateTime> _CREATED_AT;
		
		private System.Nullable<bool> _STATUS;
		
		private EntityRef<BRAND_TB> _BRAND_TB;
		
		private EntityRef<CATEGORY_TB> _CATEGORY_TB;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIDChanging(int value);
    partial void OnIDChanged();
    partial void OnBRAND_IDChanging(System.Nullable<int> value);
    partial void OnBRAND_IDChanged();
    partial void OnCATEGORY_IDChanging(System.Nullable<int> value);
    partial void OnCATEGORY_IDChanged();
    partial void OnPRODUCTChanging(string value);
    partial void OnPRODUCTChanged();
    partial void OnSELL_PRICEChanging(System.Nullable<decimal> value);
    partial void OnSELL_PRICEChanged();
    partial void OnCREATED_ATChanging(System.Nullable<System.DateTime> value);
    partial void OnCREATED_ATChanged();
    partial void OnSTATUSChanging(System.Nullable<bool> value);
    partial void OnSTATUSChanged();
    #endregion
		
		public PRODUCT_TB()
		{
			this._BRAND_TB = default(EntityRef<BRAND_TB>);
			this._CATEGORY_TB = default(EntityRef<CATEGORY_TB>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				if ((this._ID != value))
				{
					this.OnIDChanging(value);
					this.SendPropertyChanging();
					this._ID = value;
					this.SendPropertyChanged("ID");
					this.OnIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_BRAND_ID", DbType="Int")]
		public System.Nullable<int> BRAND_ID
		{
			get
			{
				return this._BRAND_ID;
			}
			set
			{
				if ((this._BRAND_ID != value))
				{
					if (this._BRAND_TB.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnBRAND_IDChanging(value);
					this.SendPropertyChanging();
					this._BRAND_ID = value;
					this.SendPropertyChanged("BRAND_ID");
					this.OnBRAND_IDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CATEGORY_ID", DbType="Int")]
		public System.Nullable<int> CATEGORY_ID
		{
			get
			{
				return this._CATEGORY_ID;
			}
			set
			{
				if ((this._CATEGORY_ID != value))
				{
					if (this._CATEGORY_TB.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnCATEGORY_IDChanging(value);
					this.SendPropertyChanging();
					this._CATEGORY_ID = value;
					this.SendPropertyChanged("CATEGORY_ID");
					this.OnCATEGORY_IDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PRODUCT", DbType="VarChar(50)")]
		public string PRODUCT
		{
			get
			{
				return this._PRODUCT;
			}
			set
			{
				if ((this._PRODUCT != value))
				{
					this.OnPRODUCTChanging(value);
					this.SendPropertyChanging();
					this._PRODUCT = value;
					this.SendPropertyChanged("PRODUCT");
					this.OnPRODUCTChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SELL_PRICE", DbType="Decimal(18,0)")]
		public System.Nullable<decimal> SELL_PRICE
		{
			get
			{
				return this._SELL_PRICE;
			}
			set
			{
				if ((this._SELL_PRICE != value))
				{
					this.OnSELL_PRICEChanging(value);
					this.SendPropertyChanging();
					this._SELL_PRICE = value;
					this.SendPropertyChanged("SELL_PRICE");
					this.OnSELL_PRICEChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CREATED_AT", DbType="DateTime")]
		public System.Nullable<System.DateTime> CREATED_AT
		{
			get
			{
				return this._CREATED_AT;
			}
			set
			{
				if ((this._CREATED_AT != value))
				{
					this.OnCREATED_ATChanging(value);
					this.SendPropertyChanging();
					this._CREATED_AT = value;
					this.SendPropertyChanged("CREATED_AT");
					this.OnCREATED_ATChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_STATUS", DbType="Bit")]
		public System.Nullable<bool> STATUS
		{
			get
			{
				return this._STATUS;
			}
			set
			{
				if ((this._STATUS != value))
				{
					this.OnSTATUSChanging(value);
					this.SendPropertyChanging();
					this._STATUS = value;
					this.SendPropertyChanged("STATUS");
					this.OnSTATUSChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="BRAND_TB_PRODUCT_TB", Storage="_BRAND_TB", ThisKey="BRAND_ID", OtherKey="ID", IsForeignKey=true, DeleteRule="CASCADE")]
		public BRAND_TB BRAND_TB
		{
			get
			{
				return this._BRAND_TB.Entity;
			}
			set
			{
				BRAND_TB previousValue = this._BRAND_TB.Entity;
				if (((previousValue != value) 
							|| (this._BRAND_TB.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._BRAND_TB.Entity = null;
						previousValue.PRODUCT_TBs.Remove(this);
					}
					this._BRAND_TB.Entity = value;
					if ((value != null))
					{
						value.PRODUCT_TBs.Add(this);
						this._BRAND_ID = value.ID;
					}
					else
					{
						this._BRAND_ID = default(Nullable<int>);
					}
					this.SendPropertyChanged("BRAND_TB");
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="CATEGORY_TB_PRODUCT_TB", Storage="_CATEGORY_TB", ThisKey="CATEGORY_ID", OtherKey="ID", IsForeignKey=true, DeleteRule="CASCADE")]
		public CATEGORY_TB CATEGORY_TB
		{
			get
			{
				return this._CATEGORY_TB.Entity;
			}
			set
			{
				CATEGORY_TB previousValue = this._CATEGORY_TB.Entity;
				if (((previousValue != value) 
							|| (this._CATEGORY_TB.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._CATEGORY_TB.Entity = null;
						previousValue.PRODUCT_TBs.Remove(this);
					}
					this._CATEGORY_TB.Entity = value;
					if ((value != null))
					{
						value.PRODUCT_TBs.Add(this);
						this._CATEGORY_ID = value.ID;
					}
					else
					{
						this._CATEGORY_ID = default(Nullable<int>);
					}
					this.SendPropertyChanged("CATEGORY_TB");
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
