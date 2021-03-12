using System;
using System.Text;
using System.Xml;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x020007E9 RID: 2025
	[Serializable]
	public class FailedMSOSyncObject : ADConfigurationObject
	{
		// Token: 0x1700236D RID: 9069
		// (get) Token: 0x06006408 RID: 25608 RVA: 0x0015B9FE File Offset: 0x00159BFE
		// (set) Token: 0x06006409 RID: 25609 RVA: 0x0015BA15 File Offset: 0x00159C15
		public SyncObjectId ObjectId
		{
			get
			{
				return (SyncObjectId)this.propertyBag[FailedMSOSyncObjectSchema.SyncObjectId];
			}
			set
			{
				this.propertyBag[FailedMSOSyncObjectSchema.SyncObjectId] = value;
			}
		}

		// Token: 0x1700236E RID: 9070
		// (get) Token: 0x0600640A RID: 25610 RVA: 0x0015BA28 File Offset: 0x00159C28
		// (set) Token: 0x0600640B RID: 25611 RVA: 0x0015BA3F File Offset: 0x00159C3F
		public DateTime? DivergenceTimestamp
		{
			get
			{
				return (DateTime?)this.propertyBag[FailedMSOSyncObjectSchema.DivergenceTimestamp];
			}
			set
			{
				this.propertyBag[FailedMSOSyncObjectSchema.DivergenceTimestamp] = value;
			}
		}

		// Token: 0x1700236F RID: 9071
		// (get) Token: 0x0600640C RID: 25612 RVA: 0x0015BA57 File Offset: 0x00159C57
		// (set) Token: 0x0600640D RID: 25613 RVA: 0x0015BA6E File Offset: 0x00159C6E
		public int DivergenceCount
		{
			get
			{
				return (int)this.propertyBag[FailedMSOSyncObjectSchema.DivergenceCount];
			}
			set
			{
				this.propertyBag[FailedMSOSyncObjectSchema.DivergenceCount] = value;
			}
		}

		// Token: 0x17002370 RID: 9072
		// (get) Token: 0x0600640E RID: 25614 RVA: 0x0015BA86 File Offset: 0x00159C86
		// (set) Token: 0x0600640F RID: 25615 RVA: 0x0015BA9D File Offset: 0x00159C9D
		public bool IsTemporary
		{
			get
			{
				return (bool)this.propertyBag[FailedMSOSyncObjectSchema.IsTemporary];
			}
			set
			{
				this.propertyBag[FailedMSOSyncObjectSchema.IsTemporary] = value;
			}
		}

		// Token: 0x17002371 RID: 9073
		// (get) Token: 0x06006410 RID: 25616 RVA: 0x0015BAB5 File Offset: 0x00159CB5
		// (set) Token: 0x06006411 RID: 25617 RVA: 0x0015BACC File Offset: 0x00159CCC
		public bool IsIncrementalOnly
		{
			get
			{
				return (bool)this.propertyBag[FailedMSOSyncObjectSchema.IsIncrementalOnly];
			}
			set
			{
				this.propertyBag[FailedMSOSyncObjectSchema.IsIncrementalOnly] = value;
			}
		}

		// Token: 0x17002372 RID: 9074
		// (get) Token: 0x06006412 RID: 25618 RVA: 0x0015BAE4 File Offset: 0x00159CE4
		// (set) Token: 0x06006413 RID: 25619 RVA: 0x0015BAFB File Offset: 0x00159CFB
		public bool IsLinkRelated
		{
			get
			{
				return (bool)this.propertyBag[FailedMSOSyncObjectSchema.IsLinkRelated];
			}
			set
			{
				this.propertyBag[FailedMSOSyncObjectSchema.IsLinkRelated] = value;
			}
		}

		// Token: 0x17002373 RID: 9075
		// (get) Token: 0x06006414 RID: 25620 RVA: 0x0015BB13 File Offset: 0x00159D13
		// (set) Token: 0x06006415 RID: 25621 RVA: 0x0015BB2A File Offset: 0x00159D2A
		public bool IsIgnoredInHaltCondition
		{
			get
			{
				return (bool)this.propertyBag[FailedMSOSyncObjectSchema.IsIgnoredInHaltCondition];
			}
			set
			{
				this.propertyBag[FailedMSOSyncObjectSchema.IsIgnoredInHaltCondition] = value;
			}
		}

		// Token: 0x17002374 RID: 9076
		// (get) Token: 0x06006416 RID: 25622 RVA: 0x0015BB42 File Offset: 0x00159D42
		// (set) Token: 0x06006417 RID: 25623 RVA: 0x0015BB59 File Offset: 0x00159D59
		public bool IsTenantWideDivergence
		{
			get
			{
				return (bool)this.propertyBag[FailedMSOSyncObjectSchema.IsTenantWideDivergence];
			}
			set
			{
				this.propertyBag[FailedMSOSyncObjectSchema.IsTenantWideDivergence] = value;
			}
		}

		// Token: 0x17002375 RID: 9077
		// (get) Token: 0x06006418 RID: 25624 RVA: 0x0015BB71 File Offset: 0x00159D71
		// (set) Token: 0x06006419 RID: 25625 RVA: 0x0015BB88 File Offset: 0x00159D88
		public bool IsValidationDivergence
		{
			get
			{
				return (bool)this.propertyBag[FailedMSOSyncObjectSchema.IsValidationDivergence];
			}
			set
			{
				this.propertyBag[FailedMSOSyncObjectSchema.IsValidationDivergence] = value;
			}
		}

		// Token: 0x17002376 RID: 9078
		// (get) Token: 0x0600641A RID: 25626 RVA: 0x0015BBA0 File Offset: 0x00159DA0
		// (set) Token: 0x0600641B RID: 25627 RVA: 0x0015BBB7 File Offset: 0x00159DB7
		public bool IsRetriable
		{
			get
			{
				return (bool)this.propertyBag[FailedMSOSyncObjectSchema.IsRetriable];
			}
			set
			{
				this.propertyBag[FailedMSOSyncObjectSchema.IsRetriable] = value;
			}
		}

		// Token: 0x17002377 RID: 9079
		// (get) Token: 0x0600641C RID: 25628 RVA: 0x0015BBCF File Offset: 0x00159DCF
		// (set) Token: 0x0600641D RID: 25629 RVA: 0x0015BBE6 File Offset: 0x00159DE6
		public MultiValuedProperty<string> Errors
		{
			get
			{
				return (MultiValuedProperty<string>)this.propertyBag[FailedMSOSyncObjectSchema.Errors];
			}
			set
			{
				this.propertyBag[FailedMSOSyncObjectSchema.Errors] = value;
			}
		}

		// Token: 0x17002378 RID: 9080
		// (get) Token: 0x0600641E RID: 25630 RVA: 0x0015BBF9 File Offset: 0x00159DF9
		// (set) Token: 0x0600641F RID: 25631 RVA: 0x0015BC0B File Offset: 0x00159E0B
		public string DivergenceInfoXml
		{
			get
			{
				return (string)this[FailedMSOSyncObjectSchema.DivergenceInfoXml];
			}
			internal set
			{
				this[FailedMSOSyncObjectSchema.DivergenceInfoXml] = value;
			}
		}

		// Token: 0x17002379 RID: 9081
		// (get) Token: 0x06006420 RID: 25632 RVA: 0x0015BC19 File Offset: 0x00159E19
		// (set) Token: 0x06006421 RID: 25633 RVA: 0x0015BC30 File Offset: 0x00159E30
		public string Comment
		{
			get
			{
				return (string)this.propertyBag[FailedMSOSyncObjectSchema.Comment];
			}
			set
			{
				this.propertyBag[FailedMSOSyncObjectSchema.Comment] = value;
			}
		}

		// Token: 0x1700237A RID: 9082
		// (get) Token: 0x06006422 RID: 25634 RVA: 0x0015BC43 File Offset: 0x00159E43
		// (set) Token: 0x06006423 RID: 25635 RVA: 0x0015BC4C File Offset: 0x00159E4C
		public string BuildNumber
		{
			get
			{
				return this.GetValueForTag(DivergenceInfo.BuildNumber);
			}
			set
			{
				this.SetValueForTag(DivergenceInfo.BuildNumber, value);
			}
		}

		// Token: 0x1700237B RID: 9083
		// (get) Token: 0x06006424 RID: 25636 RVA: 0x0015BC56 File Offset: 0x00159E56
		// (set) Token: 0x06006425 RID: 25637 RVA: 0x0015BC5F File Offset: 0x00159E5F
		public string TargetBuildNumber
		{
			get
			{
				return this.GetValueForTag(DivergenceInfo.TargetBuildNumber);
			}
			set
			{
				this.SetValueForTag(DivergenceInfo.TargetBuildNumber, value);
			}
		}

		// Token: 0x1700237C RID: 9084
		// (get) Token: 0x06006426 RID: 25638 RVA: 0x0015BC69 File Offset: 0x00159E69
		// (set) Token: 0x06006427 RID: 25639 RVA: 0x0015BC72 File Offset: 0x00159E72
		public string CmdletName
		{
			get
			{
				return this.GetValueForTag(DivergenceInfo.CmdletName);
			}
			set
			{
				this.SetValueForTag(DivergenceInfo.CmdletName, value);
			}
		}

		// Token: 0x1700237D RID: 9085
		// (get) Token: 0x06006428 RID: 25640 RVA: 0x0015BC7C File Offset: 0x00159E7C
		// (set) Token: 0x06006429 RID: 25641 RVA: 0x0015BC85 File Offset: 0x00159E85
		public string CmdletParameters
		{
			get
			{
				return this.GetValueForTag(DivergenceInfo.CmdletParameters);
			}
			set
			{
				this.SetValueForTag(DivergenceInfo.CmdletParameters, value);
			}
		}

		// Token: 0x1700237E RID: 9086
		// (get) Token: 0x0600642A RID: 25642 RVA: 0x0015BC8F File Offset: 0x00159E8F
		// (set) Token: 0x0600642B RID: 25643 RVA: 0x0015BC98 File Offset: 0x00159E98
		public string ErrorMessage
		{
			get
			{
				return this.GetValueForTag(DivergenceInfo.ErrorMessage);
			}
			set
			{
				this.SetValueForTag(DivergenceInfo.ErrorMessage, value);
			}
		}

		// Token: 0x1700237F RID: 9087
		// (get) Token: 0x0600642C RID: 25644 RVA: 0x0015BCA2 File Offset: 0x00159EA2
		// (set) Token: 0x0600642D RID: 25645 RVA: 0x0015BCAB File Offset: 0x00159EAB
		public string ErrorSymbolicName
		{
			get
			{
				return this.GetValueForTag(DivergenceInfo.ErrorSymbolicName);
			}
			set
			{
				this.SetValueForTag(DivergenceInfo.ErrorSymbolicName, value);
			}
		}

		// Token: 0x17002380 RID: 9088
		// (get) Token: 0x0600642E RID: 25646 RVA: 0x0015BCB5 File Offset: 0x00159EB5
		// (set) Token: 0x0600642F RID: 25647 RVA: 0x0015BCBE File Offset: 0x00159EBE
		public string ErrorStringId
		{
			get
			{
				return this.GetValueForTag(DivergenceInfo.ErrorStringId);
			}
			set
			{
				this.SetValueForTag(DivergenceInfo.ErrorStringId, value);
			}
		}

		// Token: 0x17002381 RID: 9089
		// (get) Token: 0x06006430 RID: 25648 RVA: 0x0015BCC8 File Offset: 0x00159EC8
		// (set) Token: 0x06006431 RID: 25649 RVA: 0x0015BCD1 File Offset: 0x00159ED1
		public string ErrorCategory
		{
			get
			{
				return this.GetValueForTag(DivergenceInfo.ErrorCategory);
			}
			set
			{
				this.SetValueForTag(DivergenceInfo.ErrorCategory, value);
			}
		}

		// Token: 0x17002382 RID: 9090
		// (get) Token: 0x06006432 RID: 25650 RVA: 0x0015BCDB File Offset: 0x00159EDB
		// (set) Token: 0x06006433 RID: 25651 RVA: 0x0015BCE4 File Offset: 0x00159EE4
		public string StreamName
		{
			get
			{
				return this.GetValueForTag(DivergenceInfo.StreamName);
			}
			set
			{
				this.SetValueForTag(DivergenceInfo.StreamName, value);
			}
		}

		// Token: 0x17002383 RID: 9091
		// (get) Token: 0x06006434 RID: 25652 RVA: 0x0015BCEE File Offset: 0x00159EEE
		// (set) Token: 0x06006435 RID: 25653 RVA: 0x0015BCF8 File Offset: 0x00159EF8
		public string MinDivergenceRetryDatetime
		{
			get
			{
				return this.GetValueForTag(DivergenceInfo.MininumRetryDatetime);
			}
			set
			{
				this.SetValueForTag(DivergenceInfo.MininumRetryDatetime, value);
			}
		}

		// Token: 0x17002384 RID: 9092
		// (get) Token: 0x06006436 RID: 25654 RVA: 0x0015BD03 File Offset: 0x00159F03
		public string ServiceInstanceId
		{
			get
			{
				return base.Id.Parent.Parent.Name;
			}
		}

		// Token: 0x17002385 RID: 9093
		// (get) Token: 0x06006437 RID: 25655 RVA: 0x0015BD1A File Offset: 0x00159F1A
		public override ObjectId Identity
		{
			get
			{
				return new CompoundSyncObjectId(this.ObjectId, new ServiceInstanceId(this.ServiceInstanceId));
			}
		}

		// Token: 0x06006438 RID: 25656 RVA: 0x0015BD34 File Offset: 0x00159F34
		public void LoadDivergenceInfoXml()
		{
			if (this.xmlDoc == null)
			{
				string divergenceInfoXml = this.DivergenceInfoXml;
				this.xmlDoc = new SafeXmlDocument();
				if (string.IsNullOrEmpty(divergenceInfoXml))
				{
					XmlElement newChild = this.xmlDoc.CreateElement("DivergenceInfo");
					this.xmlDoc.AppendChild(newChild);
					return;
				}
				try
				{
					this.xmlDoc.LoadXml(divergenceInfoXml);
				}
				catch (Exception)
				{
					ExTraceGlobals.ADTopologyTracer.TraceError((long)this.GetHashCode(), string.Format("Failed to load Xml blob: \"{0}\". Xml blob will be reset.", divergenceInfoXml));
					this.xmlDoc = null;
					this.DivergenceInfoXml = string.Empty;
					this.LoadDivergenceInfoXml();
				}
			}
		}

		// Token: 0x06006439 RID: 25657 RVA: 0x0015BDD8 File Offset: 0x00159FD8
		public void SaveDivergenceInfoXml()
		{
			if (this.xmlDoc != null)
			{
				StringBuilder stringBuilder = new StringBuilder();
				using (XmlWriter xmlWriter = XmlWriter.Create(stringBuilder))
				{
					this.xmlDoc.Save(xmlWriter);
					xmlWriter.Close();
				}
				this.DivergenceInfoXml = stringBuilder.ToString();
			}
		}

		// Token: 0x0600643A RID: 25658 RVA: 0x0015BE34 File Offset: 0x0015A034
		internal override void Initialize()
		{
			this.LoadDivergenceInfoXml();
		}

		// Token: 0x17002386 RID: 9094
		// (get) Token: 0x0600643B RID: 25659 RVA: 0x0015BE3C File Offset: 0x0015A03C
		internal override ADObjectSchema Schema
		{
			get
			{
				return FailedMSOSyncObject.SchemaObject;
			}
		}

		// Token: 0x17002387 RID: 9095
		// (get) Token: 0x0600643C RID: 25660 RVA: 0x0015BE43 File Offset: 0x0015A043
		internal override string MostDerivedObjectClass
		{
			get
			{
				return FailedMSOSyncObject.MostDerivedClass;
			}
		}

		// Token: 0x17002388 RID: 9096
		// (get) Token: 0x0600643D RID: 25661 RVA: 0x0015BE4A File Offset: 0x0015A04A
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x17002389 RID: 9097
		// (get) Token: 0x0600643E RID: 25662 RVA: 0x0015BE51 File Offset: 0x0015A051
		// (set) Token: 0x0600643F RID: 25663 RVA: 0x0015BE68 File Offset: 0x0015A068
		internal string ExternalDirectoryObjectId
		{
			get
			{
				return (string)this.propertyBag[FailedMSOSyncObjectSchema.ObjectId];
			}
			set
			{
				this.propertyBag[FailedMSOSyncObjectSchema.ObjectId] = value;
			}
		}

		// Token: 0x1700238A RID: 9098
		// (get) Token: 0x06006440 RID: 25664 RVA: 0x0015BE7B File Offset: 0x0015A07B
		// (set) Token: 0x06006441 RID: 25665 RVA: 0x0015BE92 File Offset: 0x0015A092
		internal string ExternalDirectoryOrganizationId
		{
			get
			{
				return (string)this.propertyBag[FailedMSOSyncObjectSchema.ContextId];
			}
			set
			{
				this.propertyBag[FailedMSOSyncObjectSchema.ContextId] = value;
			}
		}

		// Token: 0x06006442 RID: 25666 RVA: 0x0015BEA5 File Offset: 0x0015A0A5
		internal static string GetObjectName(SyncObjectId syncObjectId)
		{
			return string.Format("{0}{1}", FailedMSOSyncObject.GetCompactGuidString(syncObjectId.ContextId), FailedMSOSyncObject.GetCompactGuidString(syncObjectId.ObjectId));
		}

		// Token: 0x06006443 RID: 25667 RVA: 0x0015BEC8 File Offset: 0x0015A0C8
		internal static QueryFilter DivergenceTimestampFilterBuilder(SinglePropertyFilter filter)
		{
			if (!(filter is ComparisonFilter))
			{
				throw new ADFilterException(DirectoryStrings.ExceptionUnsupportedFilterForProperty(filter.Property.Name, filter.GetType(), typeof(ComparisonFilter)));
			}
			ComparisonFilter comparisonFilter = (ComparisonFilter)filter;
			if (ComparisonOperator.Like == comparisonFilter.ComparisonOperator || ComparisonOperator.IsMemberOf == comparisonFilter.ComparisonOperator)
			{
				throw new ADFilterException(DirectoryStrings.ExceptionUnsupportedOperatorForProperty(comparisonFilter.Property.Name, comparisonFilter.ComparisonOperator.ToString()));
			}
			long num = ((DateTime)comparisonFilter.PropertyValue).ToFileTimeUtc();
			return new ComparisonFilter(comparisonFilter.ComparisonOperator, FailedMSOSyncObjectSchema.DivergenceTimestampRaw, num);
		}

		// Token: 0x06006444 RID: 25668 RVA: 0x0015BF6C File Offset: 0x0015A16C
		internal static object ExternalDirectoryObjectIdGetter(IPropertyBag bag)
		{
			return FailedMSOSyncObject.ExternalDirectoryIdGetter(bag, FailedMSOSyncObjectSchema.RawObjectId);
		}

		// Token: 0x06006445 RID: 25669 RVA: 0x0015BF79 File Offset: 0x0015A179
		internal static void ExternalDirectoryObjectIdSetter(object value, IPropertyBag bag)
		{
			FailedMSOSyncObject.ExternalDirectoryIdSetter(value, bag, FailedMSOSyncObjectSchema.RawObjectId);
		}

		// Token: 0x06006446 RID: 25670 RVA: 0x0015BF87 File Offset: 0x0015A187
		internal static QueryFilter ExternalDirectoryObjectIdFilterBuilder(SinglePropertyFilter filter)
		{
			return FailedMSOSyncObject.ExternalDirectoryObjectIdFilterBuilder(filter, FailedMSOSyncObjectSchema.RawObjectId);
		}

		// Token: 0x06006447 RID: 25671 RVA: 0x0015BF94 File Offset: 0x0015A194
		internal static object ExternalDirectoryOrganizationIdGetter(IPropertyBag bag)
		{
			return FailedMSOSyncObject.ExternalDirectoryIdGetter(bag, FailedMSOSyncObjectSchema.RawContextId);
		}

		// Token: 0x06006448 RID: 25672 RVA: 0x0015BFA1 File Offset: 0x0015A1A1
		internal static void ExternalDirectoryOrganizationIdSetter(object value, IPropertyBag bag)
		{
			FailedMSOSyncObject.ExternalDirectoryIdSetter(value, bag, FailedMSOSyncObjectSchema.RawContextId);
		}

		// Token: 0x06006449 RID: 25673 RVA: 0x0015BFAF File Offset: 0x0015A1AF
		internal static QueryFilter ExternalDirectoryOrganizationIdFilterBuilder(SinglePropertyFilter filter)
		{
			return FailedMSOSyncObject.ExternalDirectoryObjectIdFilterBuilder(filter, FailedMSOSyncObjectSchema.RawContextId);
		}

		// Token: 0x0600644A RID: 25674 RVA: 0x0015BFBC File Offset: 0x0015A1BC
		private static object ExternalDirectoryIdGetter(IPropertyBag bag, ADPropertyDefinition externalDirectoryId)
		{
			object obj = bag[externalDirectoryId];
			if (obj != null)
			{
				string text = (string)obj;
				if (text.StartsWith("id:", StringComparison.OrdinalIgnoreCase))
				{
					return text.Substring("id:".Length);
				}
			}
			return obj;
		}

		// Token: 0x0600644B RID: 25675 RVA: 0x0015BFFC File Offset: 0x0015A1FC
		private string GetValueForTag(DivergenceInfo divergenceInfo)
		{
			if (this.xmlDoc != null && this.xmlDoc.DocumentElement != null)
			{
				XmlNode xmlNode = this.xmlDoc.DocumentElement.SelectSingleNode(Enum.GetName(typeof(DivergenceInfo), divergenceInfo));
				if (xmlNode != null)
				{
					return ((XmlElement)xmlNode).GetAttribute("Value");
				}
			}
			return null;
		}

		// Token: 0x0600644C RID: 25676 RVA: 0x0015C05C File Offset: 0x0015A25C
		private void SetValueForTag(DivergenceInfo divergenceInfo, string value)
		{
			if (this.xmlDoc != null)
			{
				XmlElement xmlElement = this.xmlDoc.DocumentElement;
				if (xmlElement == null)
				{
					xmlElement = this.xmlDoc.CreateElement("DivergenceInfo");
					this.xmlDoc.AppendChild(xmlElement);
				}
				string name = Enum.GetName(typeof(DivergenceInfo), divergenceInfo);
				XmlElement xmlElement2 = (XmlElement)xmlElement.SelectSingleNode(name);
				if (xmlElement2 == null)
				{
					xmlElement2 = this.xmlDoc.CreateElement(name);
					xmlElement.AppendChild(xmlElement2);
				}
				xmlElement2.SetAttribute("Value", value);
			}
		}

		// Token: 0x0600644D RID: 25677 RVA: 0x0015C0E8 File Offset: 0x0015A2E8
		private static void ExternalDirectoryIdSetter(object value, IPropertyBag bag, ADPropertyDefinition externalDirectoryId)
		{
			bag[externalDirectoryId] = value;
		}

		// Token: 0x0600644E RID: 25678 RVA: 0x0015C0FF File Offset: 0x0015A2FF
		private static string GetMangledId(object rawId)
		{
			return string.Format("{0}{1}", "id:", (string)rawId);
		}

		// Token: 0x0600644F RID: 25679 RVA: 0x0015C118 File Offset: 0x0015A318
		private static QueryFilter ExternalDirectoryObjectIdFilterBuilder(SinglePropertyFilter filter, ADPropertyDefinition externalDirectoryId)
		{
			if (!(filter is ComparisonFilter) && !(filter is TextFilter))
			{
				throw new ADFilterException(DirectoryStrings.ExceptionUnsupportedFilterForPropertyMultiple(filter.Property.Name, filter.GetType(), typeof(ComparisonFilter), typeof(TextFilter)));
			}
			ComparisonFilter comparisonFilter = filter as ComparisonFilter;
			QueryFilter queryFilter;
			if (comparisonFilter != null)
			{
				if (comparisonFilter.ComparisonOperator != ComparisonOperator.Equal && ComparisonOperator.NotEqual != comparisonFilter.ComparisonOperator)
				{
					throw new ADFilterException(DirectoryStrings.ExceptionUnsupportedOperatorForProperty(comparisonFilter.Property.Name, comparisonFilter.ComparisonOperator.ToString()));
				}
				queryFilter = new OrFilter(new QueryFilter[]
				{
					new ComparisonFilter(ComparisonOperator.Equal, externalDirectoryId, comparisonFilter.PropertyValue),
					new ComparisonFilter(ComparisonOperator.Equal, externalDirectoryId, FailedMSOSyncObject.GetMangledId(comparisonFilter.PropertyValue))
				});
				if (comparisonFilter.ComparisonOperator == ComparisonOperator.NotEqual)
				{
					queryFilter = new NotFilter(queryFilter);
				}
			}
			else
			{
				TextFilter textFilter = (TextFilter)filter;
				queryFilter = new OrFilter(new QueryFilter[]
				{
					new TextFilter(externalDirectoryId, textFilter.Text, textFilter.MatchOptions, textFilter.MatchFlags),
					new TextFilter(externalDirectoryId, FailedMSOSyncObject.GetMangledId(textFilter.Text), textFilter.MatchOptions, textFilter.MatchFlags)
				});
			}
			return queryFilter;
		}

		// Token: 0x06006450 RID: 25680 RVA: 0x0015C244 File Offset: 0x0015A444
		private static string GetCompactGuidString(string guidString)
		{
			return new Guid(guidString).ToString("N");
		}

		// Token: 0x040042B3 RID: 17075
		private const string RootNodeName = "DivergenceInfo";

		// Token: 0x040042B4 RID: 17076
		private const string ValueAttribute = "Value";

		// Token: 0x040042B5 RID: 17077
		private const string IdPrefix = "id:";

		// Token: 0x040042B6 RID: 17078
		internal static readonly string MostDerivedClass = "msExchMSOForwardSyncDivergence";

		// Token: 0x040042B7 RID: 17079
		private static readonly FailedMSOSyncObjectSchema SchemaObject = ObjectSchema.GetInstance<FailedMSOSyncObjectSchema>();

		// Token: 0x040042B8 RID: 17080
		private SafeXmlDocument xmlDoc;
	}
}
