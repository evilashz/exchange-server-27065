using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Autodiscover.WCF
{
	// Token: 0x020000B0 RID: 176
	[DataContract(Name = "DocumentSharingLocation", Namespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover")]
	public class DocumentSharingLocation
	{
		// Token: 0x06000448 RID: 1096 RVA: 0x00018020 File Offset: 0x00016220
		public DocumentSharingLocation(string serviceUrl, string locationUrl, string displayName, FileExtensionCollection supportedFileExtensions, bool externalAccessAllowed, bool anonymousAccessAllowed, bool canModifyPermissions, bool isDefault)
		{
			Common.ThrowIfNullOrEmpty(serviceUrl, "serviceUrl");
			Common.ThrowIfNullOrEmpty(locationUrl, "locationUrl");
			Common.ThrowIfNullOrEmpty(displayName, "displayName");
			if (supportedFileExtensions == null)
			{
				throw new ArgumentNullException("supportedFileExtensions");
			}
			if (supportedFileExtensions.Count == 0)
			{
				throw new ArgumentException("supportedFileExtensions must not be empty.");
			}
			this.serviceUrl = serviceUrl;
			this.locationUrl = locationUrl;
			this.displayName = displayName;
			this.supportedFileExtensions = supportedFileExtensions;
			this.externalAccessAllowed = externalAccessAllowed;
			this.anonymousAccessAllowed = anonymousAccessAllowed;
			this.canModifyPermissions = canModifyPermissions;
			this.isDefault = isDefault;
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000449 RID: 1097 RVA: 0x000180B4 File Offset: 0x000162B4
		// (set) Token: 0x0600044A RID: 1098 RVA: 0x000180BC File Offset: 0x000162BC
		[DataMember(Name = "ServiceUrl", IsRequired = true, Order = 1)]
		public string ServiceUrl
		{
			get
			{
				return this.serviceUrl;
			}
			set
			{
				this.serviceUrl = value;
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x0600044B RID: 1099 RVA: 0x000180C5 File Offset: 0x000162C5
		// (set) Token: 0x0600044C RID: 1100 RVA: 0x000180CD File Offset: 0x000162CD
		[DataMember(Name = "LocationUrl", IsRequired = true, Order = 2)]
		public string LocationUrl
		{
			get
			{
				return this.locationUrl;
			}
			set
			{
				this.locationUrl = value;
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x0600044D RID: 1101 RVA: 0x000180D6 File Offset: 0x000162D6
		// (set) Token: 0x0600044E RID: 1102 RVA: 0x000180DE File Offset: 0x000162DE
		[DataMember(Name = "DisplayName", IsRequired = true, Order = 3)]
		public string DisplayName
		{
			get
			{
				return this.displayName;
			}
			set
			{
				this.displayName = value;
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x0600044F RID: 1103 RVA: 0x000180E7 File Offset: 0x000162E7
		// (set) Token: 0x06000450 RID: 1104 RVA: 0x000180EF File Offset: 0x000162EF
		[DataMember(Name = "SupportedFileExtensions", IsRequired = true, Order = 4)]
		public FileExtensionCollection SupportedFileExtensions
		{
			get
			{
				return this.supportedFileExtensions;
			}
			set
			{
				this.supportedFileExtensions = value;
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000451 RID: 1105 RVA: 0x000180F8 File Offset: 0x000162F8
		// (set) Token: 0x06000452 RID: 1106 RVA: 0x00018100 File Offset: 0x00016300
		[DataMember(Name = "ExternalAccessAllowed", IsRequired = true, Order = 5)]
		public bool ExternalAccessAllowed
		{
			get
			{
				return this.externalAccessAllowed;
			}
			set
			{
				this.externalAccessAllowed = value;
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000453 RID: 1107 RVA: 0x00018109 File Offset: 0x00016309
		// (set) Token: 0x06000454 RID: 1108 RVA: 0x00018111 File Offset: 0x00016311
		[DataMember(Name = "AnonymousAccessAllowed", IsRequired = true, Order = 6)]
		public bool AnonymousAccessAllowed
		{
			get
			{
				return this.anonymousAccessAllowed;
			}
			set
			{
				this.anonymousAccessAllowed = value;
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000455 RID: 1109 RVA: 0x0001811A File Offset: 0x0001631A
		// (set) Token: 0x06000456 RID: 1110 RVA: 0x00018122 File Offset: 0x00016322
		[DataMember(Name = "CanModifyPermissions", IsRequired = true, Order = 7)]
		public bool CanModifyPermissions
		{
			get
			{
				return this.canModifyPermissions;
			}
			set
			{
				this.canModifyPermissions = value;
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000457 RID: 1111 RVA: 0x0001812B File Offset: 0x0001632B
		// (set) Token: 0x06000458 RID: 1112 RVA: 0x00018133 File Offset: 0x00016333
		[DataMember(Name = "IsDefault", IsRequired = true, Order = 8)]
		public bool IsDefault
		{
			get
			{
				return this.isDefault;
			}
			set
			{
				this.isDefault = value;
			}
		}

		// Token: 0x04000372 RID: 882
		private string serviceUrl;

		// Token: 0x04000373 RID: 883
		private string locationUrl;

		// Token: 0x04000374 RID: 884
		private string displayName;

		// Token: 0x04000375 RID: 885
		private FileExtensionCollection supportedFileExtensions;

		// Token: 0x04000376 RID: 886
		private bool externalAccessAllowed;

		// Token: 0x04000377 RID: 887
		private bool anonymousAccessAllowed;

		// Token: 0x04000378 RID: 888
		private bool canModifyPermissions;

		// Token: 0x04000379 RID: 889
		private bool isDefault;
	}
}
