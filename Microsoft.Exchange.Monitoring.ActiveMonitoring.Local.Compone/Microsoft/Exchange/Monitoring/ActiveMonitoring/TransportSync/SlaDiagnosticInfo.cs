using System;
using System.Text;
using System.Xml;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.TransportSync
{
	// Token: 0x02000118 RID: 280
	public class SlaDiagnosticInfo
	{
		// Token: 0x0600085E RID: 2142 RVA: 0x00031A88 File Offset: 0x0002FC88
		internal SlaDiagnosticInfo(XmlReader xmlReader)
		{
			if (xmlReader.Name != "DatabaseQueueManager")
			{
				throw new ArgumentException("Invalid Xml Node was passed in", "DatabaseQueueManager");
			}
			while (xmlReader.Read())
			{
				string name = xmlReader.Name;
				string a;
				if ((a = name) != null)
				{
					if (!(a == "databaseId"))
					{
						if (!(a == "workType"))
						{
							if (!(a == "nextPollingTime"))
							{
								if (!(a == "itemsOutOfSla"))
								{
									if (a == "itemsOutOfSlaPercent")
									{
										this.ItemsOutOfSlaPercent = new int?(int.Parse(xmlReader.ReadString()));
									}
								}
								else
								{
									this.ItemsOutOfSla = new int?(int.Parse(xmlReader.ReadString()));
								}
							}
							else
							{
								string text = xmlReader.ReadString();
								if (!string.IsNullOrEmpty(text))
								{
									this.NextPollingTime = ExDateTime.Parse(text);
								}
							}
						}
						else
						{
							this.WorkType = xmlReader.ReadString();
						}
					}
					else
					{
						this.MdbGuid = new Guid(xmlReader.ReadString());
					}
				}
				if (this.WorkType == DiagnosticInfoConstants.WorkTypeName && name == "PollingQueue")
				{
					if (ExDateTime.UtcNow.AddMinutes(-5.0) > this.NextPollingTime && !this.NextPollingTime.Equals(ExDateTime.MinValue))
					{
						this.OutOfSlaTime = ExDateTime.UtcNow.Subtract(this.NextPollingTime);
						this.IsOutOfSla = true;
						return;
					}
					break;
				}
				else if (name == "DatabaseQueueManager")
				{
					return;
				}
			}
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x0600085F RID: 2143 RVA: 0x00031C11 File Offset: 0x0002FE11
		// (set) Token: 0x06000860 RID: 2144 RVA: 0x00031C19 File Offset: 0x0002FE19
		public Guid MdbGuid { get; private set; }

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000861 RID: 2145 RVA: 0x00031C22 File Offset: 0x0002FE22
		// (set) Token: 0x06000862 RID: 2146 RVA: 0x00031C2A File Offset: 0x0002FE2A
		public bool IsOutOfSla { get; private set; }

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06000863 RID: 2147 RVA: 0x00031C33 File Offset: 0x0002FE33
		// (set) Token: 0x06000864 RID: 2148 RVA: 0x00031C3B File Offset: 0x0002FE3B
		public int? ItemsOutOfSla { get; private set; }

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06000865 RID: 2149 RVA: 0x00031C44 File Offset: 0x0002FE44
		// (set) Token: 0x06000866 RID: 2150 RVA: 0x00031C4C File Offset: 0x0002FE4C
		public int? ItemsOutOfSlaPercent { get; private set; }

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000867 RID: 2151 RVA: 0x00031C55 File Offset: 0x0002FE55
		// (set) Token: 0x06000868 RID: 2152 RVA: 0x00031C5D File Offset: 0x0002FE5D
		public ExDateTime NextPollingTime { get; private set; }

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000869 RID: 2153 RVA: 0x00031C66 File Offset: 0x0002FE66
		// (set) Token: 0x0600086A RID: 2154 RVA: 0x00031C6E File Offset: 0x0002FE6E
		public TimeSpan OutOfSlaTime { get; private set; }

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x0600086B RID: 2155 RVA: 0x00031C77 File Offset: 0x0002FE77
		// (set) Token: 0x0600086C RID: 2156 RVA: 0x00031C7F File Offset: 0x0002FE7F
		public string WorkType { get; private set; }

		// Token: 0x0600086D RID: 2157 RVA: 0x00031C88 File Offset: 0x0002FE88
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("{0}:{1}; ", "MDBGuid", this.MdbGuid);
			stringBuilder.AppendFormat("{0}:{1}; ", "WorkType", this.WorkType);
			stringBuilder.AppendFormat("{0}:{1}; ", "IsOutOfSla", this.IsOutOfSla);
			stringBuilder.AppendFormat("{0}:{1}; ", "OutOfSlaTime", this.OutOfSlaTime);
			stringBuilder.AppendFormat("{0}:{1}; ", "ItemsOutOfSla", this.ItemsOutOfSla);
			stringBuilder.AppendFormat("{0}:{1}; ", "ItemsOutOfSlaPercent", this.ItemsOutOfSlaPercent);
			return stringBuilder.ToString();
		}

		// Token: 0x040005C2 RID: 1474
		private const int DefaultOutOfSlaBufferInMinutes = -5;
	}
}
