using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000474 RID: 1140
	[XmlType("ResolveNamesType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class ResolveNamesRequest : BaseRequest
	{
		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x0600219A RID: 8602 RVA: 0x000A275E File Offset: 0x000A095E
		// (set) Token: 0x0600219B RID: 8603 RVA: 0x000A2766 File Offset: 0x000A0966
		[DataMember(Name = "ReturnFullContactData", IsRequired = false, EmitDefaultValue = false, Order = 1)]
		[XmlAttribute("ReturnFullContactData")]
		public bool ReturnFullContactData
		{
			get
			{
				return this.returnFullContactData;
			}
			set
			{
				this.returnFullContactData = value;
			}
		}

		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x0600219C RID: 8604 RVA: 0x000A276F File Offset: 0x000A096F
		// (set) Token: 0x0600219D RID: 8605 RVA: 0x000A2777 File Offset: 0x000A0977
		[IgnoreDataMember]
		[XmlAttribute("SearchScope")]
		public ResolveNamesSearchScopeType SearchScope
		{
			get
			{
				return this.searchScope;
			}
			set
			{
				this.searchScope = value;
			}
		}

		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x0600219E RID: 8606 RVA: 0x000A2780 File Offset: 0x000A0980
		// (set) Token: 0x0600219F RID: 8607 RVA: 0x000A278D File Offset: 0x000A098D
		[DataMember(Name = "SearchScope", IsRequired = false, EmitDefaultValue = false, Order = 2)]
		[XmlIgnore]
		public string SearchScopeString
		{
			get
			{
				return EnumUtilities.ToString<ResolveNamesSearchScopeType>(this.searchScope);
			}
			set
			{
				this.searchScope = EnumUtilities.Parse<ResolveNamesSearchScopeType>(value);
			}
		}

		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x060021A0 RID: 8608 RVA: 0x000A279B File Offset: 0x000A099B
		// (set) Token: 0x060021A1 RID: 8609 RVA: 0x000A27A3 File Offset: 0x000A09A3
		[XmlAttribute("ContactDataShape")]
		[IgnoreDataMember]
		public ShapeEnum ContactDataShape
		{
			get
			{
				return this.contactDataShape;
			}
			set
			{
				this.contactDataShape = value;
			}
		}

		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x060021A2 RID: 8610 RVA: 0x000A27AC File Offset: 0x000A09AC
		// (set) Token: 0x060021A3 RID: 8611 RVA: 0x000A27B9 File Offset: 0x000A09B9
		[XmlIgnore]
		[DataMember(Name = "ContactDataShape", IsRequired = false, EmitDefaultValue = false, Order = 3)]
		public string ContactDataShapeString
		{
			get
			{
				return EnumUtilities.ToString<ShapeEnum>(this.contactDataShape);
			}
			set
			{
				this.contactDataShape = EnumUtilities.Parse<ShapeEnum>(value);
			}
		}

		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x060021A4 RID: 8612 RVA: 0x000A27C7 File Offset: 0x000A09C7
		// (set) Token: 0x060021A5 RID: 8613 RVA: 0x000A27CF File Offset: 0x000A09CF
		[DataMember(Name = "UnresolvedEntry", IsRequired = true, Order = 4)]
		[XmlElement("UnresolvedEntry", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Type = typeof(string))]
		public string UnresolvedEntry
		{
			get
			{
				return this.unresolvedEntry;
			}
			set
			{
				this.unresolvedEntry = value;
			}
		}

		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x060021A6 RID: 8614 RVA: 0x000A27D8 File Offset: 0x000A09D8
		// (set) Token: 0x060021A7 RID: 8615 RVA: 0x000A27E0 File Offset: 0x000A09E0
		[XmlArrayItem("FolderId", typeof(FolderId), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		[XmlArrayItem("DistinguishedFolderId", typeof(DistinguishedFolderId), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		[DataMember(Name = "ParentFolderIds", IsRequired = false, Order = 5)]
		[XmlArray("ParentFolderIds")]
		public BaseFolderId[] ParentFolderIds { get; set; }

		// Token: 0x060021A8 RID: 8616 RVA: 0x000A27E9 File Offset: 0x000A09E9
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new ResolveNames(callContext, this);
		}

		// Token: 0x060021A9 RID: 8617 RVA: 0x000A27F2 File Offset: 0x000A09F2
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			if (this.ParentFolderIds != null)
			{
				return BaseRequest.GetServerInfoForFolderIdList(callContext, this.ParentFolderIds);
			}
			return callContext.GetServerInfoForEffectiveCaller();
		}

		// Token: 0x060021AA RID: 8618 RVA: 0x000A280F File Offset: 0x000A0A0F
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return base.GetResourceKeysFromProxyInfo(false, callContext);
		}

		// Token: 0x060021AB RID: 8619 RVA: 0x000A2819 File Offset: 0x000A0A19
		protected override List<ServiceObjectId> GetAllIds()
		{
			if (this.ParentFolderIds != null)
			{
				return new List<ServiceObjectId>(this.ParentFolderIds);
			}
			return null;
		}

		// Token: 0x0400149E RID: 5278
		internal const string ElementNameResolveNames = "ResolveNames";

		// Token: 0x0400149F RID: 5279
		internal const string ElementNameUnresolvedEntry = "UnresolvedEntry";

		// Token: 0x040014A0 RID: 5280
		internal const string ElementNameParentFolderIds = "ParentFolderIds";

		// Token: 0x040014A1 RID: 5281
		internal const string AttributeNameReturnFullContactData = "ReturnFullContactData";

		// Token: 0x040014A2 RID: 5282
		internal const string AttributeNameSearchScope = "SearchScope";

		// Token: 0x040014A3 RID: 5283
		internal const string AttributeNameContactDataShape = "ContactDataShape";

		// Token: 0x040014A4 RID: 5284
		private bool returnFullContactData;

		// Token: 0x040014A5 RID: 5285
		private ShapeEnum contactDataShape = ShapeEnum.Default;

		// Token: 0x040014A6 RID: 5286
		private string unresolvedEntry;

		// Token: 0x040014A7 RID: 5287
		private ResolveNamesSearchScopeType searchScope = ResolveNamesSearchScopeType.ActiveDirectoryContacts;
	}
}
