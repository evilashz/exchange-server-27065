using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020004BA RID: 1210
	[DataContract]
	public class UMDialingRuleEntry
	{
		// Token: 0x17002397 RID: 9111
		// (get) Token: 0x06003BBB RID: 15291 RVA: 0x000B42EC File Offset: 0x000B24EC
		// (set) Token: 0x06003BBC RID: 15292 RVA: 0x000B42F4 File Offset: 0x000B24F4
		[DataMember]
		public string Comment { get; set; }

		// Token: 0x17002398 RID: 9112
		// (get) Token: 0x06003BBD RID: 15293 RVA: 0x000B42FD File Offset: 0x000B24FD
		// (set) Token: 0x06003BBE RID: 15294 RVA: 0x000B4305 File Offset: 0x000B2505
		[DataMember]
		public string DialedNumber { get; set; }

		// Token: 0x17002399 RID: 9113
		// (get) Token: 0x06003BBF RID: 15295 RVA: 0x000B430E File Offset: 0x000B250E
		// (set) Token: 0x06003BC0 RID: 15296 RVA: 0x000B4316 File Offset: 0x000B2516
		[DataMember]
		public string GroupName { get; set; }

		// Token: 0x1700239A RID: 9114
		// (get) Token: 0x06003BC1 RID: 15297 RVA: 0x000B431F File Offset: 0x000B251F
		// (set) Token: 0x06003BC2 RID: 15298 RVA: 0x000B4327 File Offset: 0x000B2527
		[DataMember]
		public string NumberMask { get; set; }

		// Token: 0x06003BC3 RID: 15299 RVA: 0x000B4330 File Offset: 0x000B2530
		public UMDialingRuleEntry()
		{
		}

		// Token: 0x06003BC4 RID: 15300 RVA: 0x000B4338 File Offset: 0x000B2538
		public UMDialingRuleEntry(DialGroupEntry entry)
		{
			this.Comment = ((entry.Comment == null) ? string.Empty : entry.Comment);
			this.DialedNumber = entry.DialedNumber;
			this.GroupName = entry.Name;
			this.NumberMask = entry.NumberMask;
		}

		// Token: 0x06003BC5 RID: 15301 RVA: 0x000B4392 File Offset: 0x000B2592
		public static IEnumerable<UMDialingRuleEntry> GetUMDialingRuleEntry(MultiValuedProperty<DialGroupEntry> entries)
		{
			if (entries != null)
			{
				return from entry in entries
				select new UMDialingRuleEntry(entry);
			}
			return null;
		}

		// Token: 0x06003BC6 RID: 15302 RVA: 0x000B43E0 File Offset: 0x000B25E0
		public static string[] GetStringArray(IEnumerable<UMDialingRuleEntry> entries)
		{
			if (entries != null)
			{
				return (from entry in entries
				select new DialGroupEntry(entry.GroupName, entry.NumberMask, entry.DialedNumber, entry.Comment).ToString()).ToArray<string>();
			}
			return null;
		}
	}
}
