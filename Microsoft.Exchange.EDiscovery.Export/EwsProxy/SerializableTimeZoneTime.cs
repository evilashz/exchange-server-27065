using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200027C RID: 636
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class SerializableTimeZoneTime
	{
		// Token: 0x1700085B RID: 2139
		// (get) Token: 0x06001787 RID: 6023 RVA: 0x0002765B File Offset: 0x0002585B
		// (set) Token: 0x06001788 RID: 6024 RVA: 0x00027663 File Offset: 0x00025863
		public int Bias
		{
			get
			{
				return this.biasField;
			}
			set
			{
				this.biasField = value;
			}
		}

		// Token: 0x1700085C RID: 2140
		// (get) Token: 0x06001789 RID: 6025 RVA: 0x0002766C File Offset: 0x0002586C
		// (set) Token: 0x0600178A RID: 6026 RVA: 0x00027674 File Offset: 0x00025874
		public string Time
		{
			get
			{
				return this.timeField;
			}
			set
			{
				this.timeField = value;
			}
		}

		// Token: 0x1700085D RID: 2141
		// (get) Token: 0x0600178B RID: 6027 RVA: 0x0002767D File Offset: 0x0002587D
		// (set) Token: 0x0600178C RID: 6028 RVA: 0x00027685 File Offset: 0x00025885
		public short DayOrder
		{
			get
			{
				return this.dayOrderField;
			}
			set
			{
				this.dayOrderField = value;
			}
		}

		// Token: 0x1700085E RID: 2142
		// (get) Token: 0x0600178D RID: 6029 RVA: 0x0002768E File Offset: 0x0002588E
		// (set) Token: 0x0600178E RID: 6030 RVA: 0x00027696 File Offset: 0x00025896
		public short Month
		{
			get
			{
				return this.monthField;
			}
			set
			{
				this.monthField = value;
			}
		}

		// Token: 0x1700085F RID: 2143
		// (get) Token: 0x0600178F RID: 6031 RVA: 0x0002769F File Offset: 0x0002589F
		// (set) Token: 0x06001790 RID: 6032 RVA: 0x000276A7 File Offset: 0x000258A7
		public string DayOfWeek
		{
			get
			{
				return this.dayOfWeekField;
			}
			set
			{
				this.dayOfWeekField = value;
			}
		}

		// Token: 0x17000860 RID: 2144
		// (get) Token: 0x06001791 RID: 6033 RVA: 0x000276B0 File Offset: 0x000258B0
		// (set) Token: 0x06001792 RID: 6034 RVA: 0x000276B8 File Offset: 0x000258B8
		public string Year
		{
			get
			{
				return this.yearField;
			}
			set
			{
				this.yearField = value;
			}
		}

		// Token: 0x04000FE0 RID: 4064
		private int biasField;

		// Token: 0x04000FE1 RID: 4065
		private string timeField;

		// Token: 0x04000FE2 RID: 4066
		private short dayOrderField;

		// Token: 0x04000FE3 RID: 4067
		private short monthField;

		// Token: 0x04000FE4 RID: 4068
		private string dayOfWeekField;

		// Token: 0x04000FE5 RID: 4069
		private string yearField;
	}
}
