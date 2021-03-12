using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.Data.Services.Common;

namespace Microsoft.WindowsAzure.ActiveDirectory
{
	// Token: 0x02000591 RID: 1425
	[DataServiceKey("objectId")]
	public class DirectoryObject
	{
		// Token: 0x0600135E RID: 4958 RVA: 0x0002B910 File Offset: 0x00029B10
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public static DirectoryObject CreateDirectoryObject(string objectId)
		{
			return new DirectoryObject
			{
				objectId = objectId
			};
		}

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x0600135F RID: 4959 RVA: 0x0002B92B File Offset: 0x00029B2B
		// (set) Token: 0x06001360 RID: 4960 RVA: 0x0002B933 File Offset: 0x00029B33
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string objectType
		{
			get
			{
				return this._objectType;
			}
			set
			{
				this._objectType = value;
			}
		}

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06001361 RID: 4961 RVA: 0x0002B93C File Offset: 0x00029B3C
		// (set) Token: 0x06001362 RID: 4962 RVA: 0x0002B944 File Offset: 0x00029B44
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string objectId
		{
			get
			{
				return this._objectId;
			}
			set
			{
				this._objectId = value;
			}
		}

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06001363 RID: 4963 RVA: 0x0002B94D File Offset: 0x00029B4D
		// (set) Token: 0x06001364 RID: 4964 RVA: 0x0002B955 File Offset: 0x00029B55
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public DateTime? softDeletionTimestamp
		{
			get
			{
				return this._softDeletionTimestamp;
			}
			set
			{
				this._softDeletionTimestamp = value;
			}
		}

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x06001365 RID: 4965 RVA: 0x0002B95E File Offset: 0x00029B5E
		// (set) Token: 0x06001366 RID: 4966 RVA: 0x0002B966 File Offset: 0x00029B66
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public DirectoryObject createdOnBehalfOf
		{
			get
			{
				return this._createdOnBehalfOf;
			}
			set
			{
				this._createdOnBehalfOf = value;
			}
		}

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x06001367 RID: 4967 RVA: 0x0002B96F File Offset: 0x00029B6F
		// (set) Token: 0x06001368 RID: 4968 RVA: 0x0002B977 File Offset: 0x00029B77
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public Collection<DirectoryObject> createdObjects
		{
			get
			{
				return this._createdObjects;
			}
			set
			{
				if (value != null)
				{
					this._createdObjects = value;
				}
			}
		}

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x06001369 RID: 4969 RVA: 0x0002B983 File Offset: 0x00029B83
		// (set) Token: 0x0600136A RID: 4970 RVA: 0x0002B98B File Offset: 0x00029B8B
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public DirectoryObject manager
		{
			get
			{
				return this._manager;
			}
			set
			{
				this._manager = value;
			}
		}

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x0600136B RID: 4971 RVA: 0x0002B994 File Offset: 0x00029B94
		// (set) Token: 0x0600136C RID: 4972 RVA: 0x0002B99C File Offset: 0x00029B9C
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public Collection<DirectoryObject> directReports
		{
			get
			{
				return this._directReports;
			}
			set
			{
				if (value != null)
				{
					this._directReports = value;
				}
			}
		}

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x0600136D RID: 4973 RVA: 0x0002B9A8 File Offset: 0x00029BA8
		// (set) Token: 0x0600136E RID: 4974 RVA: 0x0002B9B0 File Offset: 0x00029BB0
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public Collection<DirectoryObject> members
		{
			get
			{
				return this._members;
			}
			set
			{
				if (value != null)
				{
					this._members = value;
				}
			}
		}

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x0600136F RID: 4975 RVA: 0x0002B9BC File Offset: 0x00029BBC
		// (set) Token: 0x06001370 RID: 4976 RVA: 0x0002B9C4 File Offset: 0x00029BC4
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public Collection<DirectoryObject> memberOf
		{
			get
			{
				return this._memberOf;
			}
			set
			{
				if (value != null)
				{
					this._memberOf = value;
				}
			}
		}

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x06001371 RID: 4977 RVA: 0x0002B9D0 File Offset: 0x00029BD0
		// (set) Token: 0x06001372 RID: 4978 RVA: 0x0002B9D8 File Offset: 0x00029BD8
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public Collection<DirectoryObject> owners
		{
			get
			{
				return this._owners;
			}
			set
			{
				if (value != null)
				{
					this._owners = value;
				}
			}
		}

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x06001373 RID: 4979 RVA: 0x0002B9E4 File Offset: 0x00029BE4
		// (set) Token: 0x06001374 RID: 4980 RVA: 0x0002B9EC File Offset: 0x00029BEC
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public Collection<DirectoryObject> ownedObjects
		{
			get
			{
				return this._ownedObjects;
			}
			set
			{
				if (value != null)
				{
					this._ownedObjects = value;
				}
			}
		}

		// Token: 0x040018C1 RID: 6337
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _objectType;

		// Token: 0x040018C2 RID: 6338
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _objectId;

		// Token: 0x040018C3 RID: 6339
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private DateTime? _softDeletionTimestamp;

		// Token: 0x040018C4 RID: 6340
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private DirectoryObject _createdOnBehalfOf;

		// Token: 0x040018C5 RID: 6341
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<DirectoryObject> _createdObjects = new Collection<DirectoryObject>();

		// Token: 0x040018C6 RID: 6342
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private DirectoryObject _manager;

		// Token: 0x040018C7 RID: 6343
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<DirectoryObject> _directReports = new Collection<DirectoryObject>();

		// Token: 0x040018C8 RID: 6344
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<DirectoryObject> _members = new Collection<DirectoryObject>();

		// Token: 0x040018C9 RID: 6345
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<DirectoryObject> _memberOf = new Collection<DirectoryObject>();

		// Token: 0x040018CA RID: 6346
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<DirectoryObject> _owners = new Collection<DirectoryObject>();

		// Token: 0x040018CB RID: 6347
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<DirectoryObject> _ownedObjects = new Collection<DirectoryObject>();
	}
}
