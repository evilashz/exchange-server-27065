using System;
using System.Globalization;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020003DD RID: 989
	[XmlInclude(typeof(SendConnectionSettingsInfo))]
	[XmlType(TypeName = "ConnectionSettingsInfo", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DataContract]
	[KnownType(typeof(SendConnectionSettingsInfo))]
	[Serializable]
	public class ConnectionSettingsInfo
	{
		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06001BC6 RID: 7110 RVA: 0x0009D818 File Offset: 0x0009BA18
		// (set) Token: 0x06001BC7 RID: 7111 RVA: 0x0009D820 File Offset: 0x0009BA20
		[XmlElement("SendConnectionSettings")]
		[DataMember]
		public SendConnectionSettingsInfo SendConnectionSettings { get; set; }

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06001BC8 RID: 7112 RVA: 0x0009D829 File Offset: 0x0009BA29
		// (set) Token: 0x06001BC9 RID: 7113 RVA: 0x0009D831 File Offset: 0x0009BA31
		[DataMember]
		[XmlElement("ConnectionType")]
		public ConnectionSettingsInfoType ConnectionType { get; set; }

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06001BCA RID: 7114 RVA: 0x0009D83A File Offset: 0x0009BA3A
		// (set) Token: 0x06001BCB RID: 7115 RVA: 0x0009D842 File Offset: 0x0009BA42
		[XmlElement("ServerName")]
		[DataMember]
		public string ServerName { get; set; }

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06001BCC RID: 7116 RVA: 0x0009D84B File Offset: 0x0009BA4B
		// (set) Token: 0x06001BCD RID: 7117 RVA: 0x0009D853 File Offset: 0x0009BA53
		[XmlElement("Port")]
		[DataMember]
		public int Port { get; set; }

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06001BCE RID: 7118 RVA: 0x0009D85C File Offset: 0x0009BA5C
		// (set) Token: 0x06001BCF RID: 7119 RVA: 0x0009D864 File Offset: 0x0009BA64
		[DataMember]
		[XmlElement("Authentication")]
		public string Authentication { get; set; }

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06001BD0 RID: 7120 RVA: 0x0009D86D File Offset: 0x0009BA6D
		// (set) Token: 0x06001BD1 RID: 7121 RVA: 0x0009D875 File Offset: 0x0009BA75
		[DataMember]
		[XmlElement("Office365UserFound")]
		public bool Office365UserFound { get; set; }

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06001BD2 RID: 7122 RVA: 0x0009D87E File Offset: 0x0009BA7E
		// (set) Token: 0x06001BD3 RID: 7123 RVA: 0x0009D886 File Offset: 0x0009BA86
		[XmlElement("Security")]
		[DataMember]
		public string Security { get; set; }

		// Token: 0x06001BD4 RID: 7124 RVA: 0x0009D890 File Offset: 0x0009BA90
		public string ToMultiLineString(string lineSeparator)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("Receive connection settings:{0}Type={1}", lineSeparator, this.ConnectionType);
			ConnectionSettingsInfoType connectionType = this.ConnectionType;
			if (connectionType == ConnectionSettingsInfoType.ExchangeActiveSync || connectionType == ConnectionSettingsInfoType.Imap || connectionType == ConnectionSettingsInfoType.Pop)
			{
				stringBuilder.AppendFormat(",{0}", lineSeparator);
				stringBuilder.AppendFormat("Server name={0},{1}", this.ServerName, lineSeparator);
				stringBuilder.AppendFormat("Port={0},{1}", this.Port, lineSeparator);
				stringBuilder.AppendFormat("Authentication={0},{1}", this.Authentication, lineSeparator);
				stringBuilder.AppendFormat("Security={0}", this.Security);
				if (this.SendConnectionSettings != null)
				{
					stringBuilder.AppendFormat(".{0}", lineSeparator);
					stringBuilder.Append(this.SendConnectionSettings.ToMultiLineString(lineSeparator));
				}
				return stringBuilder.ToString();
			}
			throw new NotSupportedException(string.Format(CultureInfo.InvariantCulture, "Unexpected connection settings type encountered when converting to public representation: {0}", new object[]
			{
				this.ConnectionType
			}));
		}

		// Token: 0x06001BD5 RID: 7125 RVA: 0x0009D989 File Offset: 0x0009BB89
		public override string ToString()
		{
			return this.ToMultiLineString(" ");
		}
	}
}
