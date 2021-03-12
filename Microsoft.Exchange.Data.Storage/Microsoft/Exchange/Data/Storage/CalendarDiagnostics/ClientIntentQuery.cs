using System;
using System.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.CalendarDiagnostics
{
	// Token: 0x02000364 RID: 868
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class ClientIntentQuery
	{
		// Token: 0x17000CDA RID: 3290
		// (get) Token: 0x06002694 RID: 9876 RVA: 0x0009A95A File Offset: 0x00098B5A
		// (set) Token: 0x06002695 RID: 9877 RVA: 0x0009A962 File Offset: 0x00098B62
		public GlobalObjectId GlobalObjectId { get; private set; }

		// Token: 0x17000CDB RID: 3291
		// (get) Token: 0x06002696 RID: 9878 RVA: 0x0009A96B File Offset: 0x00098B6B
		// (set) Token: 0x06002697 RID: 9879 RVA: 0x0009A973 File Offset: 0x00098B73
		public ICalendarItemStateDefinition TargetState { get; private set; }

		// Token: 0x06002698 RID: 9880 RVA: 0x0009A97C File Offset: 0x00098B7C
		protected ClientIntentQuery(GlobalObjectId globalObjectId, ICalendarItemStateDefinition targetState)
		{
			Util.ThrowOnNullArgument(targetState, "targetState");
			this.GlobalObjectId = globalObjectId;
			this.TargetState = targetState;
		}

		// Token: 0x06002699 RID: 9881 RVA: 0x0009A9B8 File Offset: 0x00098BB8
		public static bool CheckDesiredClientIntent(ClientIntentFlags? changeIntent, params ClientIntentFlags[] desiredClientIntent)
		{
			if (desiredClientIntent == null || desiredClientIntent.Length == 0)
			{
				throw new InvalidOperationException("At least one intent should be desired from the query.");
			}
			return changeIntent != null && desiredClientIntent.Any((ClientIntentFlags flag) => ClientIntentQuery.CheckDesiredClientIntent(changeIntent.Value, flag));
		}

		// Token: 0x0600269A RID: 9882
		public abstract ClientIntentQuery.QueryResult Execute(MailboxSession session, CalendarVersionStoreGateway cvsGateway);

		// Token: 0x0600269B RID: 9883 RVA: 0x0009AA06 File Offset: 0x00098C06
		protected ClientIntentFlags GetClientIntentFromPropertyBag(PropertyBag propertyBag)
		{
			return propertyBag.GetValueOrDefault<ClientIntentFlags>(CalendarItemBaseSchema.ClientIntent, ClientIntentFlags.None);
		}

		// Token: 0x0600269C RID: 9884 RVA: 0x0009AA14 File Offset: 0x00098C14
		protected VersionedId GetIdFromPropertyBag(PropertyBag propertyBag)
		{
			return propertyBag.GetValueOrDefault<VersionedId>(ItemSchema.Id);
		}

		// Token: 0x0600269D RID: 9885 RVA: 0x0009AA24 File Offset: 0x00098C24
		protected void RunQuery(MailboxSession session, CalendarVersionStoreGateway cvsGateway, Func<PropertyBag, bool> processRecord)
		{
			if (this.TargetState.IsRecurringMasterSpecific)
			{
				cvsGateway.QueryByCleanGlobalObjectId(session, this.GlobalObjectId, this.TargetState.SchemaKey, this.TargetState.RequiredProperties, processRecord, true, ClientIntentQuery.CalendarItemClassArray, null, null);
				return;
			}
			cvsGateway.QueryByGlobalObjectId(session, this.GlobalObjectId, this.TargetState.SchemaKey, this.TargetState.RequiredProperties, processRecord, true, ClientIntentQuery.CalendarItemClassArray, null, null);
		}

		// Token: 0x0600269E RID: 9886 RVA: 0x0009AAB7 File Offset: 0x00098CB7
		private static bool CheckDesiredClientIntent(ClientIntentFlags changeIntent, ClientIntentFlags desiredFlag)
		{
			if (desiredFlag != ClientIntentFlags.None)
			{
				return changeIntent.Includes(desiredFlag);
			}
			return changeIntent == ClientIntentFlags.None;
		}

		// Token: 0x04001700 RID: 5888
		protected static readonly string[] CalendarItemClassArray = new string[]
		{
			"IPM.Appointment"
		};

		// Token: 0x02000365 RID: 869
		internal struct QueryResult
		{
			// Token: 0x060026A0 RID: 9888 RVA: 0x0009AAEA File Offset: 0x00098CEA
			public QueryResult(ClientIntentFlags? intent, VersionedId sourceVersionId)
			{
				this.Intent = intent;
				this.SourceVersionId = sourceVersionId;
			}

			// Token: 0x04001703 RID: 5891
			public ClientIntentFlags? Intent;

			// Token: 0x04001704 RID: 5892
			public VersionedId SourceVersionId;
		}
	}
}
