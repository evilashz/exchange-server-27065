using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Storage.VersionedXml
{
	// Token: 0x02000ED6 RID: 3798
	[TextMessagingSettingsRoot]
	[Serializable]
	public class TextMessagingSettingsVersion1Point0 : TextMessagingSettingsBase
	{
		// Token: 0x06008320 RID: 33568 RVA: 0x0023A7D2 File Offset: 0x002389D2
		public TextMessagingSettingsVersion1Point0() : base(new Version(1, 0))
		{
		}

		// Token: 0x06008321 RID: 33569 RVA: 0x0023A7E1 File Offset: 0x002389E1
		public TextMessagingSettingsVersion1Point0(MachineToPersonMessagingPolicies m2pMessagingPolicies, IEnumerable<DeliveryPoint> deliveryPoints) : base(new Version(1, 0))
		{
			this.MachineToPersonMessagingPolicies = m2pMessagingPolicies;
			if (deliveryPoints != null)
			{
				this.DeliveryPoints = new List<DeliveryPoint>(deliveryPoints);
			}
		}

		// Token: 0x170022CD RID: 8909
		// (get) Token: 0x06008322 RID: 33570 RVA: 0x0023A806 File Offset: 0x00238A06
		// (set) Token: 0x06008323 RID: 33571 RVA: 0x0023A813 File Offset: 0x00238A13
		[XmlElement("MachineToPersonMessagingPolicies")]
		public MachineToPersonMessagingPolicies MachineToPersonMessagingPolicies
		{
			get
			{
				return AccessorTemplates.DefaultConstructionPropertyGetter<MachineToPersonMessagingPolicies>(ref this.m2pMessagingPolicies);
			}
			set
			{
				this.m2pMessagingPolicies = value;
			}
		}

		// Token: 0x170022CE RID: 8910
		// (get) Token: 0x06008324 RID: 33572 RVA: 0x0023A81C File Offset: 0x00238A1C
		// (set) Token: 0x06008325 RID: 33573 RVA: 0x0023A829 File Offset: 0x00238A29
		[XmlElement("DeliveryPoint")]
		public List<DeliveryPoint> DeliveryPoints
		{
			get
			{
				return AccessorTemplates.ListPropertyGetter<DeliveryPoint>(ref this.deliveryPoints);
			}
			set
			{
				AccessorTemplates.ListPropertySetter<DeliveryPoint>(ref this.deliveryPoints, value);
			}
		}

		// Token: 0x170022CF RID: 8911
		// (get) Token: 0x06008326 RID: 33574 RVA: 0x0023A837 File Offset: 0x00238A37
		[XmlIgnore]
		public IList<DeliveryPoint> PersonToPersonPreferences
		{
			get
			{
				return DeliveryPoint.GetPersonToPersonPreferences(this.DeliveryPoints);
			}
		}

		// Token: 0x170022D0 RID: 8912
		// (get) Token: 0x06008327 RID: 33575 RVA: 0x0023A844 File Offset: 0x00238A44
		[XmlIgnore]
		public IList<DeliveryPoint> MachineToPersonPreferences
		{
			get
			{
				return DeliveryPoint.GetMachineToPersonPreferences(this.DeliveryPoints);
			}
		}

		// Token: 0x040057E0 RID: 22496
		private MachineToPersonMessagingPolicies m2pMessagingPolicies;

		// Token: 0x040057E1 RID: 22497
		private List<DeliveryPoint> deliveryPoints;
	}
}
