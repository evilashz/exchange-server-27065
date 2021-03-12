using System;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.SchemaConverter;

namespace Microsoft.Exchange.AirSync.SchemaConverter.XSO
{
	// Token: 0x0200022B RID: 555
	[Serializable]
	internal class XsoOrganizerEmailProperty : XsoStringProperty
	{
		// Token: 0x060014DC RID: 5340 RVA: 0x00079496 File Offset: 0x00077696
		public XsoOrganizerEmailProperty() : base(CalendarItemBaseSchema.OrganizerEmailAddress)
		{
		}

		// Token: 0x17000750 RID: 1872
		// (get) Token: 0x060014DD RID: 5341 RVA: 0x000794A4 File Offset: 0x000776A4
		public override string StringData
		{
			get
			{
				CalendarItemBase calendarItemBase = base.XsoItem as CalendarItemBase;
				if (null == calendarItemBase.Organizer)
				{
					AirSyncDiagnostics.TraceDebug(ExTraceGlobals.XsoTracer, this, "value for Organizer is null.");
					return string.Empty;
				}
				AirSyncDiagnostics.TraceDebug(ExTraceGlobals.XsoTracer, this, "Loading value for Organizer .");
				return EmailAddressConverter.LookupEmailAddressString(calendarItemBase.Organizer, calendarItemBase.Session.MailboxOwner);
			}
		}

		// Token: 0x060014DE RID: 5342 RVA: 0x00079507 File Offset: 0x00077707
		protected override void InternalSetToDefault(IProperty srcProperty)
		{
		}
	}
}
