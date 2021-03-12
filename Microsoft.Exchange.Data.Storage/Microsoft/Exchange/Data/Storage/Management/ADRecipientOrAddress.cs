using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A5A RID: 2650
	[Serializable]
	public sealed class ADRecipientOrAddress : ISerializable
	{
		// Token: 0x060060BE RID: 24766 RVA: 0x00197ECD File Offset: 0x001960CD
		internal ADRecipientOrAddress(Participant participant)
		{
			this.participant = participant;
		}

		// Token: 0x060060BF RID: 24767 RVA: 0x00197EDC File Offset: 0x001960DC
		public ADRecipientOrAddress(SerializationInfo info, StreamingContext context)
		{
			string displayName = (string)info.GetValue("displayName", typeof(string));
			string emailAddress = (string)info.GetValue("address", typeof(string));
			string routingType = (string)info.GetValue("routingType", typeof(string));
			this.participant = new Participant(displayName, emailAddress, routingType);
		}

		// Token: 0x17001AA4 RID: 6820
		// (get) Token: 0x060060C0 RID: 24768 RVA: 0x00197F4E File Offset: 0x0019614E
		internal Participant Participant
		{
			get
			{
				return this.participant;
			}
		}

		// Token: 0x17001AA5 RID: 6821
		// (get) Token: 0x060060C1 RID: 24769 RVA: 0x00197F56 File Offset: 0x00196156
		public string Address
		{
			get
			{
				return this.participant.EmailAddress ?? string.Empty;
			}
		}

		// Token: 0x17001AA6 RID: 6822
		// (get) Token: 0x060060C2 RID: 24770 RVA: 0x00197F6C File Offset: 0x0019616C
		public string DisplayName
		{
			get
			{
				return this.participant.DisplayName ?? string.Empty;
			}
		}

		// Token: 0x17001AA7 RID: 6823
		// (get) Token: 0x060060C3 RID: 24771 RVA: 0x00197F82 File Offset: 0x00196182
		public string RoutingType
		{
			get
			{
				return this.participant.RoutingType ?? string.Empty;
			}
		}

		// Token: 0x060060C4 RID: 24772 RVA: 0x00197F98 File Offset: 0x00196198
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("displayName", this.DisplayName);
			info.AddValue("address", this.Address);
			info.AddValue("routingType", this.RoutingType);
		}

		// Token: 0x060060C5 RID: 24773 RVA: 0x00197FCD File Offset: 0x001961CD
		public override string ToString()
		{
			return this.participant.ToString(AddressFormat.OutlookFormat);
		}

		// Token: 0x060060C6 RID: 24774 RVA: 0x00197FDC File Offset: 0x001961DC
		public override bool Equals(object obj)
		{
			ADRecipientOrAddress adrecipientOrAddress = obj as ADRecipientOrAddress;
			return adrecipientOrAddress != null && object.Equals(this.participant, adrecipientOrAddress.participant);
		}

		// Token: 0x060060C7 RID: 24775 RVA: 0x00198006 File Offset: 0x00196206
		public override int GetHashCode()
		{
			return this.participant.GetHashCode();
		}

		// Token: 0x040036FF RID: 14079
		private const string DisplayNameKey = "displayName";

		// Token: 0x04003700 RID: 14080
		private const string AddressKey = "address";

		// Token: 0x04003701 RID: 14081
		private const string RoutingTypeKey = "routingType";

		// Token: 0x04003702 RID: 14082
		[NonSerialized]
		private Participant participant;
	}
}
