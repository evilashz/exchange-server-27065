using System;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;

namespace Microsoft.Exchange.AirSync.SchemaConverter.AirSync
{
	// Token: 0x02000160 RID: 352
	[Serializable]
	internal class AirSyncReminder160Property : AirSyncReminder141Property, IReminder160Property, IIntegerProperty, IProperty
	{
		// Token: 0x0600100D RID: 4109 RVA: 0x0005B2FC File Offset: 0x000594FC
		public AirSyncReminder160Property(string xmlNodeNamespace, string airSyncTagName, bool requiresClientSupport) : base(xmlNodeNamespace, airSyncTagName, requiresClientSupport)
		{
		}

		// Token: 0x17000613 RID: 1555
		// (get) Token: 0x0600100E RID: 4110 RVA: 0x0005B307 File Offset: 0x00059507
		public bool ReminderIsSet
		{
			get
			{
				return base.State != PropertyState.SetToDefault && !string.IsNullOrEmpty(base.XmlNode.InnerText);
			}
		}

		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x0600100F RID: 4111 RVA: 0x0005B327 File Offset: 0x00059527
		public override int IntegerData
		{
			get
			{
				if (this.ReminderIsSet)
				{
					return base.IntegerData;
				}
				return -1;
			}
		}
	}
}
