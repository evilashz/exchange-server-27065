using System;
using System.Linq;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.DxStore.Common
{
	// Token: 0x0200008C RID: 140
	[DataContract(Namespace = "http://www.outlook.com/highavailability/dxstore/v1/")]
	public class WcfTimeout
	{
		// Token: 0x17000168 RID: 360
		// (get) Token: 0x0600055D RID: 1373 RVA: 0x00012DB0 File Offset: 0x00010FB0
		// (set) Token: 0x0600055E RID: 1374 RVA: 0x00012DB8 File Offset: 0x00010FB8
		[DataMember]
		public TimeSpan? Open { get; set; }

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x0600055F RID: 1375 RVA: 0x00012DC1 File Offset: 0x00010FC1
		// (set) Token: 0x06000560 RID: 1376 RVA: 0x00012DC9 File Offset: 0x00010FC9
		[DataMember]
		public TimeSpan? Close { get; set; }

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06000561 RID: 1377 RVA: 0x00012DD2 File Offset: 0x00010FD2
		// (set) Token: 0x06000562 RID: 1378 RVA: 0x00012DDA File Offset: 0x00010FDA
		[DataMember]
		public TimeSpan? Send { get; set; }

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000563 RID: 1379 RVA: 0x00012DE3 File Offset: 0x00010FE3
		// (set) Token: 0x06000564 RID: 1380 RVA: 0x00012DEB File Offset: 0x00010FEB
		[DataMember]
		public TimeSpan? Receive { get; set; }

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000565 RID: 1381 RVA: 0x00012DF4 File Offset: 0x00010FF4
		// (set) Token: 0x06000566 RID: 1382 RVA: 0x00012DFC File Offset: 0x00010FFC
		[DataMember]
		public TimeSpan? Operation { get; set; }

		// Token: 0x06000567 RID: 1383 RVA: 0x00012E08 File Offset: 0x00011008
		public static TimeSpan? StringToTimeSpan(string tsStr, TimeSpan? defaultTs)
		{
			TimeSpan value;
			if (!TimeSpan.TryParse(tsStr, out value))
			{
				return defaultTs;
			}
			return new TimeSpan?(value);
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x00012E30 File Offset: 0x00011030
		public static WcfTimeout Parse(string wcfTimeoutStr, WcfTimeout defaultTimeout = null)
		{
			WcfTimeout wcfTimeout = (defaultTimeout != null) ? defaultTimeout.Clone() : new WcfTimeout();
			string[] array = wcfTimeoutStr.Split(new char[]
			{
				';'
			});
			foreach (string text in array)
			{
				string[] array3 = (from s in text.Split(new char[]
				{
					'='
				})
				select s.Trim()).ToArray<string>();
				if (array3.Length > 1)
				{
					string text2 = array3[0];
					string tsStr = array3[1];
					string a;
					if ((a = text2) != null)
					{
						if (!(a == "open"))
						{
							if (!(a == "close"))
							{
								if (!(a == "send"))
								{
									if (!(a == "receive"))
									{
										if (a == "operation")
										{
											wcfTimeout.Receive = WcfTimeout.StringToTimeSpan(tsStr, wcfTimeout.Receive);
										}
									}
									else
									{
										wcfTimeout.Receive = WcfTimeout.StringToTimeSpan(tsStr, wcfTimeout.Receive);
									}
								}
								else
								{
									wcfTimeout.Send = WcfTimeout.StringToTimeSpan(tsStr, wcfTimeout.Send);
								}
							}
							else
							{
								wcfTimeout.Close = WcfTimeout.StringToTimeSpan(tsStr, wcfTimeout.Close);
							}
						}
						else
						{
							wcfTimeout.Open = WcfTimeout.StringToTimeSpan(tsStr, wcfTimeout.Open);
						}
					}
				}
			}
			return wcfTimeout;
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x00012F92 File Offset: 0x00011192
		public WcfTimeout Clone()
		{
			return (WcfTimeout)base.MemberwiseClone();
		}
	}
}
