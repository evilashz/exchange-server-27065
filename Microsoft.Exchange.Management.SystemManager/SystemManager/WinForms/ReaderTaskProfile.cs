using System;
using System.Data;
using Microsoft.Exchange.Configuration.MonadDataProvider;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020000B3 RID: 179
	public class ReaderTaskProfile : TaskProfileBase
	{
		// Token: 0x17000153 RID: 339
		// (get) Token: 0x060005CD RID: 1485 RVA: 0x00015C8C File Offset: 0x00013E8C
		// (set) Token: 0x060005CE RID: 1486 RVA: 0x00015C94 File Offset: 0x00013E94
		[DDIDataObjectNameExist]
		public string DataObjectName
		{
			get
			{
				return this.dataObjectName;
			}
			set
			{
				this.dataObjectName = value;
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x060005CF RID: 1487 RVA: 0x00015C9D File Offset: 0x00013E9D
		public object DataObject
		{
			get
			{
				return (base.Runner as Reader).DataObject;
			}
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x00015CAF File Offset: 0x00013EAF
		internal override void Run(CommandInteractionHandler interactionHandler, DataRow row, DataObjectStore store)
		{
			(base.Runner as Reader).Run(interactionHandler, row, store);
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x00015CC4 File Offset: 0x00013EC4
		public bool HasPermission()
		{
			return (base.Runner as Reader).HasPermission(base.GetEffectiveParameters());
		}

		// Token: 0x040001DF RID: 479
		private string dataObjectName;
	}
}
