using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.Data.Storage.VersionedXml
{
	// Token: 0x02000ED5 RID: 3797
	[Serializable]
	public class DeliveryPoint
	{
		// Token: 0x06008307 RID: 33543 RVA: 0x0023A558 File Offset: 0x00238758
		internal static IList<DeliveryPoint> GetPersonToPersonPreferences(IList<DeliveryPoint> candidates)
		{
			List<DeliveryPoint> list = new List<DeliveryPoint>(candidates.Count);
			foreach (DeliveryPoint deliveryPoint in candidates)
			{
				if (deliveryPoint.Ready && -1 != deliveryPoint.P2pMessagingPriority)
				{
					list.Add(deliveryPoint);
				}
			}
			list.Sort((DeliveryPoint x, DeliveryPoint y) => x.P2pMessagingPriority.CompareTo(y.P2pMessagingPriority));
			return new ReadOnlyCollection<DeliveryPoint>(list);
		}

		// Token: 0x06008308 RID: 33544 RVA: 0x0023A60C File Offset: 0x0023880C
		internal static IList<DeliveryPoint> GetMachineToPersonPreferences(IList<DeliveryPoint> candidates)
		{
			List<DeliveryPoint> list = new List<DeliveryPoint>(candidates.Count);
			foreach (DeliveryPoint deliveryPoint in candidates)
			{
				if (deliveryPoint.Ready && -1 != deliveryPoint.M2pMessagingPriority)
				{
					list.Add(deliveryPoint);
				}
			}
			list.Sort((DeliveryPoint x, DeliveryPoint y) => x.M2pMessagingPriority.CompareTo(y.M2pMessagingPriority));
			return new ReadOnlyCollection<DeliveryPoint>(list);
		}

		// Token: 0x06008309 RID: 33545 RVA: 0x0023A69C File Offset: 0x0023889C
		public DeliveryPoint()
		{
		}

		// Token: 0x0600830A RID: 33546 RVA: 0x0023A6A4 File Offset: 0x002388A4
		public DeliveryPoint(byte identity, DeliveryPointType type, E164Number phonenumber, string protocol, string deviceType, string deviceId, string deviceFriendlyName, int p2pMessagingPriority, int m2pMessagingPriority)
		{
			this.Identity = identity;
			this.Type = type;
			this.PhoneNumber = phonenumber;
			this.Protocol = protocol;
			this.DeviceType = deviceType;
			this.DeviceId = deviceId;
			this.DeviceFriendlyName = deviceFriendlyName;
			this.P2pMessagingPriority = p2pMessagingPriority;
			this.M2pMessagingPriority = m2pMessagingPriority;
		}

		// Token: 0x170022C3 RID: 8899
		// (get) Token: 0x0600830B RID: 33547 RVA: 0x0023A6FC File Offset: 0x002388FC
		// (set) Token: 0x0600830C RID: 33548 RVA: 0x0023A704 File Offset: 0x00238904
		[XmlElement("Identity")]
		public byte Identity { get; set; }

		// Token: 0x170022C4 RID: 8900
		// (get) Token: 0x0600830D RID: 33549 RVA: 0x0023A70D File Offset: 0x0023890D
		// (set) Token: 0x0600830E RID: 33550 RVA: 0x0023A715 File Offset: 0x00238915
		[XmlElement("Type")]
		public DeliveryPointType Type { get; set; }

		// Token: 0x170022C5 RID: 8901
		// (get) Token: 0x0600830F RID: 33551 RVA: 0x0023A71E File Offset: 0x0023891E
		// (set) Token: 0x06008310 RID: 33552 RVA: 0x0023A726 File Offset: 0x00238926
		[XmlElement("PhoneNumber")]
		public E164Number PhoneNumber { get; set; }

		// Token: 0x170022C6 RID: 8902
		// (get) Token: 0x06008311 RID: 33553 RVA: 0x0023A72F File Offset: 0x0023892F
		// (set) Token: 0x06008312 RID: 33554 RVA: 0x0023A737 File Offset: 0x00238937
		[XmlElement("Protocol")]
		public string Protocol { get; set; }

		// Token: 0x170022C7 RID: 8903
		// (get) Token: 0x06008313 RID: 33555 RVA: 0x0023A740 File Offset: 0x00238940
		// (set) Token: 0x06008314 RID: 33556 RVA: 0x0023A748 File Offset: 0x00238948
		[XmlElement("DeviceType")]
		public string DeviceType { get; set; }

		// Token: 0x170022C8 RID: 8904
		// (get) Token: 0x06008315 RID: 33557 RVA: 0x0023A751 File Offset: 0x00238951
		// (set) Token: 0x06008316 RID: 33558 RVA: 0x0023A759 File Offset: 0x00238959
		[XmlElement("DeviceId")]
		public string DeviceId { get; set; }

		// Token: 0x170022C9 RID: 8905
		// (get) Token: 0x06008317 RID: 33559 RVA: 0x0023A762 File Offset: 0x00238962
		// (set) Token: 0x06008318 RID: 33560 RVA: 0x0023A76A File Offset: 0x0023896A
		[XmlElement("DeviceFriendlyName")]
		public string DeviceFriendlyName { get; set; }

		// Token: 0x170022CA RID: 8906
		// (get) Token: 0x06008319 RID: 33561 RVA: 0x0023A773 File Offset: 0x00238973
		// (set) Token: 0x0600831A RID: 33562 RVA: 0x0023A77B File Offset: 0x0023897B
		[XmlElement("P2pMessaginPriority")]
		public int P2pMessagingPriority { get; set; }

		// Token: 0x170022CB RID: 8907
		// (get) Token: 0x0600831B RID: 33563 RVA: 0x0023A784 File Offset: 0x00238984
		// (set) Token: 0x0600831C RID: 33564 RVA: 0x0023A78C File Offset: 0x0023898C
		[XmlElement("M2pMessagingPriority")]
		public int M2pMessagingPriority { get; set; }

		// Token: 0x170022CC RID: 8908
		// (get) Token: 0x0600831D RID: 33565 RVA: 0x0023A798 File Offset: 0x00238998
		[XmlIgnore]
		public bool Ready
		{
			get
			{
				switch (this.Type)
				{
				case DeliveryPointType.Unknown:
					return false;
				case DeliveryPointType.ExchangeActiveSync:
					return null != this.PhoneNumber;
				case DeliveryPointType.SmtpToSmsGateway:
					return true;
				default:
					return false;
				}
			}
		}

		// Token: 0x040057D4 RID: 22484
		internal const int PriorityDisabled = -1;
	}
}
