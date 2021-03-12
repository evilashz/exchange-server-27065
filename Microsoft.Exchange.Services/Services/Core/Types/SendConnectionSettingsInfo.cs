using System;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020003DF RID: 991
	[DataContract]
	[XmlType(TypeName = "SmtpConnectionSettings", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class SendConnectionSettingsInfo
	{
		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06001BD7 RID: 7127 RVA: 0x0009D99E File Offset: 0x0009BB9E
		public ConnectionSettingsInfoType ConnectionType
		{
			get
			{
				return ConnectionSettingsInfoType.Smtp;
			}
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06001BD8 RID: 7128 RVA: 0x0009D9A2 File Offset: 0x0009BBA2
		// (set) Token: 0x06001BD9 RID: 7129 RVA: 0x0009D9AA File Offset: 0x0009BBAA
		[XmlElement("ServerName")]
		[DataMember]
		public string ServerName { get; set; }

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06001BDA RID: 7130 RVA: 0x0009D9B3 File Offset: 0x0009BBB3
		// (set) Token: 0x06001BDB RID: 7131 RVA: 0x0009D9BB File Offset: 0x0009BBBB
		[XmlElement("Port")]
		[DataMember]
		public int Port { get; set; }

		// Token: 0x06001BDC RID: 7132 RVA: 0x0009D9C4 File Offset: 0x0009BBC4
		public string ToMultiLineString(string lineSeparator)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("Send connection settings:{0}", lineSeparator);
			stringBuilder.AppendFormat("Server name={0},{1}", this.ServerName, lineSeparator);
			stringBuilder.AppendFormat("Port={0}.", this.Port);
			return stringBuilder.ToString();
		}

		// Token: 0x06001BDD RID: 7133 RVA: 0x0009DA14 File Offset: 0x0009BC14
		public override string ToString()
		{
			return this.ToMultiLineString(" ");
		}
	}
}
