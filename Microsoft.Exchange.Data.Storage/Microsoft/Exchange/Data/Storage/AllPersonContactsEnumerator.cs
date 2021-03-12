using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000479 RID: 1145
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class AllPersonContactsEnumerator : IEnumerable<IStorePropertyBag>, IEnumerable
	{
		// Token: 0x0600331E RID: 13086 RVA: 0x000CFB50 File Offset: 0x000CDD50
		protected AllPersonContactsEnumerator(MailboxSession session, ICollection<PropertyDefinition> columns)
		{
			this.session = session;
			this.columns = columns;
		}

		// Token: 0x1700100D RID: 4109
		// (get) Token: 0x0600331F RID: 13087 RVA: 0x000CFB66 File Offset: 0x000CDD66
		protected MailboxSession Session
		{
			get
			{
				return this.session;
			}
		}

		// Token: 0x1700100E RID: 4110
		// (get) Token: 0x06003320 RID: 13088 RVA: 0x000CFB6E File Offset: 0x000CDD6E
		protected ICollection<PropertyDefinition> Columns
		{
			get
			{
				return this.columns;
			}
		}

		// Token: 0x06003321 RID: 13089 RVA: 0x000CFB76 File Offset: 0x000CDD76
		public static AllPersonContactsEnumerator Create(MailboxSession session, PersonId personId, ICollection<PropertyDefinition> columns)
		{
			Util.ThrowOnNullArgument(session, "session");
			Util.ThrowOnNullArgument(personId, "personId");
			Util.ThrowOnNullArgument(columns, "columns");
			return new AllPersonContactsEnumerator.ByPersonId(session, personId, columns);
		}

		// Token: 0x06003322 RID: 13090
		public abstract IEnumerator<IStorePropertyBag> GetEnumerator();

		// Token: 0x06003323 RID: 13091 RVA: 0x000CFBA1 File Offset: 0x000CDDA1
		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotSupportedException("Must use the generics interface of GetEnumerator.");
		}

		// Token: 0x04001B98 RID: 7064
		private const int ContactBatchSize = 100;

		// Token: 0x04001B99 RID: 7065
		private static readonly Trace Tracer = ExTraceGlobals.PersonTracer;

		// Token: 0x04001B9A RID: 7066
		private readonly MailboxSession session;

		// Token: 0x04001B9B RID: 7067
		private readonly ICollection<PropertyDefinition> columns;

		// Token: 0x0200047A RID: 1146
		private sealed class ByPersonId : AllPersonContactsEnumerator
		{
			// Token: 0x06003325 RID: 13093 RVA: 0x000CFBB9 File Offset: 0x000CDDB9
			public ByPersonId(MailboxSession session, PersonId personId, ICollection<PropertyDefinition> columns) : base(session, columns)
			{
				this.personId = personId;
			}

			// Token: 0x06003326 RID: 13094 RVA: 0x000CFED4 File Offset: 0x000CE0D4
			public override IEnumerator<IStorePropertyBag> GetEnumerator()
			{
				AllPersonContactsEnumerator.Tracer.TraceDebug<PersonId>(PersonId.TraceId(this.personId), "AllPersonContactsEnumerator.GetEnumerator: this.personId = {0}", this.personId);
				ComparisonFilter filterByPersonId = new ComparisonFilter(ComparisonOperator.Equal, InternalSchema.MapiConversationId, this.personId.GetBytes());
				Folder rootFolder = Folder.Bind(base.Session, DefaultFolderType.Configuration, Array<PropertyDefinition>.Empty);
				QueryResult queryResult = ((CoreFolder)rootFolder.CoreObject).QueryExecutor.InternalItemQuery(ContentsTableFlags.ShowConversationMembers, filterByPersonId, null, QueryExclusionType.Row, this.columns, null);
				for (;;)
				{
					AllPersonContactsEnumerator.Tracer.TraceDebug<int>(PersonId.TraceId(this.personId), "AllPersonContactsEnumerator.GetEnumerator: querying for {0} more property bags", 100);
					IStorePropertyBag[] contacts = queryResult.GetPropertyBags(100);
					if (contacts == null || contacts.Length == 0)
					{
						break;
					}
					foreach (IStorePropertyBag contact in contacts)
					{
						AllPersonContactsEnumerator.Tracer.TraceDebug(PersonId.TraceId(this.personId), "AllPersonContactsEnumerator.GetEnumerator: found property bag");
						yield return contact;
					}
				}
				AllPersonContactsEnumerator.Tracer.TraceDebug(PersonId.TraceId(this.personId), "AllPersonContactsEnumerator.GetEnumerator: no more property bags");
				yield break;
				yield break;
			}

			// Token: 0x04001B9C RID: 7068
			private readonly PersonId personId;
		}
	}
}
