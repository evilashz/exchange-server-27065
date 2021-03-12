using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Storage.Infoworker.MailboxSearch;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020003DC RID: 988
	[DataContract]
	public class StartMailboxSearchParameters : WebServiceParameters
	{
		// Token: 0x17001FFB RID: 8187
		// (get) Token: 0x060032FD RID: 13053 RVA: 0x0009E3D9 File Offset: 0x0009C5D9
		public override string AssociatedCmdlet
		{
			get
			{
				return "Start-MailboxSearch";
			}
		}

		// Token: 0x17001FFC RID: 8188
		// (get) Token: 0x060032FE RID: 13054 RVA: 0x0009E3E0 File Offset: 0x0009C5E0
		public override string RbacScope
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17001FFD RID: 8189
		// (get) Token: 0x060032FF RID: 13055 RVA: 0x0009E3E7 File Offset: 0x0009C5E7
		// (set) Token: 0x06003300 RID: 13056 RVA: 0x0009E403 File Offset: 0x0009C603
		[DataMember]
		public bool Resume
		{
			get
			{
				return (bool)(base[SearchObjectSchema.Resume] ?? false);
			}
			set
			{
				base[SearchObjectSchema.Resume] = value;
			}
		}

		// Token: 0x17001FFE RID: 8190
		// (get) Token: 0x06003301 RID: 13057 RVA: 0x0009E416 File Offset: 0x0009C616
		// (set) Token: 0x06003302 RID: 13058 RVA: 0x0009E437 File Offset: 0x0009C637
		[DataMember]
		public int StatisticsStartIndex
		{
			get
			{
				return (int)(base[MailboxDiscoverySearchSchema.StatisticsStartIndex.Name] ?? 1);
			}
			set
			{
				base[MailboxDiscoverySearchSchema.StatisticsStartIndex.Name] = value;
			}
		}

		// Token: 0x17001FFF RID: 8191
		// (get) Token: 0x06003303 RID: 13059 RVA: 0x0009E44F File Offset: 0x0009C64F
		// (set) Token: 0x06003304 RID: 13060 RVA: 0x0009E457 File Offset: 0x0009C657
		[DataMember]
		public bool Force
		{
			get
			{
				return this.ShouldContinue;
			}
			set
			{
				this.ShouldContinue = value;
			}
		}
	}
}
