using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Sync;

namespace Microsoft.Exchange.Management.ForwardSyncTasks
{
	// Token: 0x02000367 RID: 871
	[Serializable]
	public class MsoRawObject : ConfigurableObject
	{
		// Token: 0x06001E57 RID: 7767 RVA: 0x00083BC4 File Offset: 0x00081DC4
		private string GetXmlElementString(XmlElement xmlElement)
		{
			StringWriter stringWriter = new StringWriter();
			XmlTextWriter w = new XmlTextWriter(stringWriter);
			xmlElement.WriteTo(w);
			return stringWriter.ToString();
		}

		// Token: 0x06001E58 RID: 7768 RVA: 0x00083BEC File Offset: 0x00081DEC
		private string SerializeForRpsDelivery(object obj)
		{
			StringWriter stringWriter = new StringWriter();
			XmlTextWriter xmlWriter = new XmlTextWriter(stringWriter);
			XmlSerializer xmlSerializer = new XmlSerializer(obj.GetType());
			xmlSerializer.Serialize(xmlWriter, obj);
			return stringWriter.ToString();
		}

		// Token: 0x06001E59 RID: 7769 RVA: 0x00083C20 File Offset: 0x00081E20
		private MultiValuedProperty<string> CollectCapabilities(XmlValueAssignedPlan[] assignedPlans)
		{
			MultiValuedProperty<string> multiValuedProperty = new MultiValuedProperty<string>();
			foreach (XmlValueAssignedPlan xmlValueAssignedPlan in assignedPlans)
			{
				multiValuedProperty.TryAdd(this.GetXmlElementString(xmlValueAssignedPlan.Plan.Capability));
			}
			return multiValuedProperty;
		}

		// Token: 0x06001E5A RID: 7770 RVA: 0x00083C60 File Offset: 0x00081E60
		private MultiValuedProperty<string> CollectErrorDetails(XmlValueValidationError[] validationErrors)
		{
			MultiValuedProperty<string> multiValuedProperty = new MultiValuedProperty<string>();
			foreach (XmlValueValidationError xmlValueValidationError in validationErrors)
			{
				multiValuedProperty.TryAdd(this.GetXmlElementString(xmlValueValidationError.ErrorInfo.ErrorDetail));
			}
			return multiValuedProperty;
		}

		// Token: 0x06001E5B RID: 7771 RVA: 0x00083CA0 File Offset: 0x00081EA0
		internal MsoRawObject(SyncObjectId externalObjectId, string serviceInstanceId, DirectoryObjectsAndLinks directoryObjectsAndLinks, bool? allLinksCollected, bool populateRawObject) : this()
		{
			this.ExternalObjectId = externalObjectId;
			this.ServiceInstanceId = serviceInstanceId;
			this.SerializedObjectAndLinks = this.SerializeForRpsDelivery(directoryObjectsAndLinks);
			this.MySyncObject = SyncObject.Create(directoryObjectsAndLinks.Objects[0], directoryObjectsAndLinks.Links, Guid.Empty);
			if (populateRawObject)
			{
				this.ObjectAndLinks = directoryObjectsAndLinks;
			}
			if (allLinksCollected != null)
			{
				this.AllLinksCollected = allLinksCollected;
				this.LinksCollected = new int?((directoryObjectsAndLinks.Links == null) ? 0 : directoryObjectsAndLinks.Links.Length);
			}
			switch (externalObjectId.ObjectClass)
			{
			case DirectoryObjectClass.Account:
			{
				Account account = (Account)directoryObjectsAndLinks.Objects[0];
				if (account.DisplayName != null)
				{
					this.DisplayName = account.DisplayName.Value[0];
					return;
				}
				break;
			}
			case DirectoryObjectClass.Company:
			{
				Company company = (Company)directoryObjectsAndLinks.Objects[0];
				if (company.DisplayName != null)
				{
					this.DisplayName = company.DisplayName.Value[0];
				}
				if (company.AssignedPlan != null)
				{
					this.AssignedPlanCapabilities = this.CollectCapabilities(company.AssignedPlan.Value);
					return;
				}
				break;
			}
			case DirectoryObjectClass.Contact:
			{
				Contact contact = (Contact)directoryObjectsAndLinks.Objects[0];
				if (contact.DisplayName != null)
				{
					this.DisplayName = contact.DisplayName.Value[0];
				}
				if (contact.ValidationError != null && contact.ValidationError.Value != null)
				{
					this.ExchangeValidationError = this.CollectErrorDetails(contact.ValidationError.Value);
					return;
				}
				break;
			}
			case DirectoryObjectClass.Device:
			case DirectoryObjectClass.KeyGroup:
			case DirectoryObjectClass.ServicePrincipal:
			case DirectoryObjectClass.SubscribedPlan:
				break;
			case DirectoryObjectClass.ForeignPrincipal:
			{
				ForeignPrincipal foreignPrincipal = (ForeignPrincipal)directoryObjectsAndLinks.Objects[0];
				if (foreignPrincipal.DisplayName != null)
				{
					this.DisplayName = foreignPrincipal.DisplayName.Value[0];
					return;
				}
				break;
			}
			case DirectoryObjectClass.Group:
			{
				Group group = (Group)directoryObjectsAndLinks.Objects[0];
				if (group.DisplayName != null)
				{
					this.DisplayName = group.DisplayName.Value[0];
				}
				if (group.ValidationError != null && group.ValidationError.Value != null)
				{
					this.ExchangeValidationError = this.CollectErrorDetails(group.ValidationError.Value);
				}
				break;
			}
			case DirectoryObjectClass.User:
			{
				User user = (User)directoryObjectsAndLinks.Objects[0];
				if (user.DisplayName != null)
				{
					this.DisplayName = user.DisplayName.Value[0];
				}
				if (user.WindowsLiveNetId != null)
				{
					this.WindowsLiveNetId = new NetID(user.WindowsLiveNetId.Value[0]);
				}
				if (user.AssignedPlan != null)
				{
					this.AssignedPlanCapabilities = this.CollectCapabilities(user.AssignedPlan.Value);
				}
				if (user.ValidationError != null && user.ValidationError.Value != null)
				{
					this.ExchangeValidationError = this.CollectErrorDetails(user.ValidationError.Value);
					return;
				}
				break;
			}
			default:
				return;
			}
		}

		// Token: 0x06001E5C RID: 7772 RVA: 0x00083F56 File Offset: 0x00082156
		internal MsoRawObject() : base(new SimpleProviderPropertyBag())
		{
			this.propertyBag.SetField(this.propertyBag.ObjectVersionPropertyDefinition, ExchangeObjectVersion.Exchange2010);
		}

		// Token: 0x1700089F RID: 2207
		// (get) Token: 0x06001E5D RID: 7773 RVA: 0x00083F7F File Offset: 0x0008217F
		// (set) Token: 0x06001E5E RID: 7774 RVA: 0x00083F87 File Offset: 0x00082187
		internal SyncObject MySyncObject
		{
			get
			{
				return this.mySyncObject;
			}
			private set
			{
				this.mySyncObject = value;
			}
		}

		// Token: 0x06001E5F RID: 7775 RVA: 0x00083F90 File Offset: 0x00082190
		public void PopulateSyncObjectData()
		{
			this.SyncObjectData = string.Empty;
			SortedDictionary<string, object> sortedDictionary = new SortedDictionary<string, object>();
			foreach (PropertyDefinition propertyDefinition in this.MySyncObject.Schema.AllProperties)
			{
				SyncPropertyDefinition syncPropertyDefinition = propertyDefinition as SyncPropertyDefinition;
				if (syncPropertyDefinition != null && this.MySyncObject.IsPropertyPresent(syncPropertyDefinition))
				{
					object obj = this.MySyncObject[syncPropertyDefinition];
					ISyncProperty syncProperty = obj as ISyncProperty;
					if (syncProperty != null)
					{
						if (!syncProperty.HasValue)
						{
							continue;
						}
						obj = syncProperty.GetValue();
					}
					if (obj != null)
					{
						if (SuppressingPiiContext.NeedPiiSuppression)
						{
							obj = SuppressingPiiProperty.TryRedact(syncPropertyDefinition, obj, null);
						}
						IList list = obj as IList;
						if (list != null)
						{
							StringBuilder stringBuilder = new StringBuilder();
							stringBuilder.Append("(");
							for (int i = 0; i < list.Count; i++)
							{
								if (i == 0)
								{
									stringBuilder.Append(list[i].ToString());
								}
								else
								{
									stringBuilder.AppendFormat(",{0}", list[i].ToString());
								}
							}
							stringBuilder.Append(")");
							obj = stringBuilder.ToString();
						}
						sortedDictionary[propertyDefinition.Name] = obj;
					}
				}
			}
			StringBuilder stringBuilder2 = new StringBuilder();
			foreach (string text in sortedDictionary.Keys)
			{
				object obj2 = sortedDictionary[text];
				stringBuilder2.AppendFormat("[{0}]:{1}\r\n", text, obj2.ToString());
			}
			this.SyncObjectData = stringBuilder2.ToString();
		}

		// Token: 0x170008A0 RID: 2208
		// (get) Token: 0x06001E60 RID: 7776 RVA: 0x00084178 File Offset: 0x00082378
		// (set) Token: 0x06001E61 RID: 7777 RVA: 0x0008418A File Offset: 0x0008238A
		public string SyncObjectData
		{
			get
			{
				return (string)this[MsoRawObjectSchema.SyncObjectData];
			}
			private set
			{
				this[MsoRawObjectSchema.SyncObjectData] = value;
			}
		}

		// Token: 0x170008A1 RID: 2209
		// (get) Token: 0x06001E62 RID: 7778 RVA: 0x00084198 File Offset: 0x00082398
		// (set) Token: 0x06001E63 RID: 7779 RVA: 0x000841AA File Offset: 0x000823AA
		public SyncObjectId ExternalObjectId
		{
			get
			{
				return (SyncObjectId)this[MsoRawObjectSchema.ExternalObjectId];
			}
			private set
			{
				this[MsoRawObjectSchema.ExternalObjectId] = value;
			}
		}

		// Token: 0x170008A2 RID: 2210
		// (get) Token: 0x06001E64 RID: 7780 RVA: 0x000841B8 File Offset: 0x000823B8
		// (set) Token: 0x06001E65 RID: 7781 RVA: 0x000841CA File Offset: 0x000823CA
		public string ServiceInstanceId
		{
			get
			{
				return (string)this[MsoRawObjectSchema.ServiceInstanceId];
			}
			private set
			{
				this[MsoRawObjectSchema.ServiceInstanceId] = value;
			}
		}

		// Token: 0x170008A3 RID: 2211
		// (get) Token: 0x06001E66 RID: 7782 RVA: 0x000841D8 File Offset: 0x000823D8
		// (set) Token: 0x06001E67 RID: 7783 RVA: 0x000841EA File Offset: 0x000823EA
		public DirectoryObjectsAndLinks ObjectAndLinks
		{
			get
			{
				return (DirectoryObjectsAndLinks)this[MsoRawObjectSchema.ObjectAndLinks];
			}
			private set
			{
				this[MsoRawObjectSchema.ObjectAndLinks] = value;
			}
		}

		// Token: 0x170008A4 RID: 2212
		// (get) Token: 0x06001E68 RID: 7784 RVA: 0x000841F8 File Offset: 0x000823F8
		// (set) Token: 0x06001E69 RID: 7785 RVA: 0x0008420A File Offset: 0x0008240A
		public string SerializedObjectAndLinks
		{
			get
			{
				return (string)this[MsoRawObjectSchema.SerializedObjectAndLinks];
			}
			private set
			{
				this[MsoRawObjectSchema.SerializedObjectAndLinks] = value;
			}
		}

		// Token: 0x170008A5 RID: 2213
		// (get) Token: 0x06001E6A RID: 7786 RVA: 0x00084218 File Offset: 0x00082418
		// (set) Token: 0x06001E6B RID: 7787 RVA: 0x0008422A File Offset: 0x0008242A
		public string DisplayName
		{
			get
			{
				return (string)this[MsoRawObjectSchema.DisplayName];
			}
			private set
			{
				this[MsoRawObjectSchema.DisplayName] = value;
			}
		}

		// Token: 0x170008A6 RID: 2214
		// (get) Token: 0x06001E6C RID: 7788 RVA: 0x00084238 File Offset: 0x00082438
		// (set) Token: 0x06001E6D RID: 7789 RVA: 0x0008424A File Offset: 0x0008244A
		public NetID WindowsLiveNetId
		{
			get
			{
				return (NetID)this[MsoRawObjectSchema.WindowsLiveNetId];
			}
			private set
			{
				this[MsoRawObjectSchema.WindowsLiveNetId] = value;
			}
		}

		// Token: 0x170008A7 RID: 2215
		// (get) Token: 0x06001E6E RID: 7790 RVA: 0x00084258 File Offset: 0x00082458
		// (set) Token: 0x06001E6F RID: 7791 RVA: 0x0008426A File Offset: 0x0008246A
		public bool? AllLinksCollected
		{
			get
			{
				return (bool?)this[MsoRawObjectSchema.AllLinksCollected];
			}
			private set
			{
				this[MsoRawObjectSchema.AllLinksCollected] = value;
			}
		}

		// Token: 0x170008A8 RID: 2216
		// (get) Token: 0x06001E70 RID: 7792 RVA: 0x0008427D File Offset: 0x0008247D
		// (set) Token: 0x06001E71 RID: 7793 RVA: 0x0008428F File Offset: 0x0008248F
		public int? LinksCollected
		{
			get
			{
				return (int?)this[MsoRawObjectSchema.LinksCollected];
			}
			private set
			{
				this[MsoRawObjectSchema.LinksCollected] = value;
			}
		}

		// Token: 0x170008A9 RID: 2217
		// (get) Token: 0x06001E72 RID: 7794 RVA: 0x000842A2 File Offset: 0x000824A2
		// (set) Token: 0x06001E73 RID: 7795 RVA: 0x000842B4 File Offset: 0x000824B4
		public MultiValuedProperty<string> AssignedPlanCapabilities
		{
			get
			{
				return (MultiValuedProperty<string>)this[MsoRawObjectSchema.AssignedPlanCapabilities];
			}
			private set
			{
				this[MsoRawObjectSchema.AssignedPlanCapabilities] = value;
			}
		}

		// Token: 0x170008AA RID: 2218
		// (get) Token: 0x06001E74 RID: 7796 RVA: 0x000842C2 File Offset: 0x000824C2
		// (set) Token: 0x06001E75 RID: 7797 RVA: 0x000842D4 File Offset: 0x000824D4
		public MultiValuedProperty<string> ExchangeValidationError
		{
			get
			{
				return (MultiValuedProperty<string>)this[MsoRawObjectSchema.ExchangeValidationError];
			}
			private set
			{
				this[MsoRawObjectSchema.ExchangeValidationError] = value;
			}
		}

		// Token: 0x170008AB RID: 2219
		// (get) Token: 0x06001E76 RID: 7798 RVA: 0x000842E2 File Offset: 0x000824E2
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return MsoRawObject.schema;
			}
		}

		// Token: 0x0400191F RID: 6431
		private static MsoRawObjectSchema schema = ObjectSchema.GetInstance<MsoRawObjectSchema>();

		// Token: 0x04001920 RID: 6432
		[NonSerialized]
		private SyncObject mySyncObject;
	}
}
