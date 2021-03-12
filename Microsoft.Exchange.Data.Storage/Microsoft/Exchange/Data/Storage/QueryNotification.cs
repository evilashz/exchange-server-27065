using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200070D RID: 1805
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class QueryNotification : Notification
	{
		// Token: 0x06004768 RID: 18280 RVA: 0x0012FAD9 File Offset: 0x0012DCD9
		internal QueryNotification(QueryNotificationType eventType, int errorCode, byte[] index, byte[] prior, ICollection<PropertyDefinition> propertyDefinitions, object[] row) : base(NotificationType.Query)
		{
			this.propertyDefinitions = propertyDefinitions;
			this.eventType = eventType;
			this.errorCode = errorCode;
			this.index = index;
			this.prior = prior;
			this.row = row;
		}

		// Token: 0x170014C8 RID: 5320
		// (get) Token: 0x06004769 RID: 18281 RVA: 0x0012FB13 File Offset: 0x0012DD13
		public QueryNotificationType EventType
		{
			get
			{
				return this.eventType;
			}
		}

		// Token: 0x170014C9 RID: 5321
		// (get) Token: 0x0600476A RID: 18282 RVA: 0x0012FB1B File Offset: 0x0012DD1B
		public int ErrorCode
		{
			get
			{
				return this.errorCode;
			}
		}

		// Token: 0x170014CA RID: 5322
		// (get) Token: 0x0600476B RID: 18283 RVA: 0x0012FB23 File Offset: 0x0012DD23
		public byte[] Index
		{
			get
			{
				return this.index;
			}
		}

		// Token: 0x170014CB RID: 5323
		// (get) Token: 0x0600476C RID: 18284 RVA: 0x0012FB2B File Offset: 0x0012DD2B
		public byte[] Prior
		{
			get
			{
				return this.prior;
			}
		}

		// Token: 0x170014CC RID: 5324
		// (get) Token: 0x0600476D RID: 18285 RVA: 0x0012FB33 File Offset: 0x0012DD33
		public object[] Row
		{
			get
			{
				return this.row;
			}
		}

		// Token: 0x170014CD RID: 5325
		// (get) Token: 0x0600476E RID: 18286 RVA: 0x0012FB3B File Offset: 0x0012DD3B
		public ICollection<PropertyDefinition> PropertyDefinitions
		{
			get
			{
				return this.propertyDefinitions;
			}
		}

		// Token: 0x0600476F RID: 18287 RVA: 0x0012FB43 File Offset: 0x0012DD43
		public static QueryNotification CreateQueryResultChangedNotification()
		{
			return new QueryNotification(QueryNotificationType.QueryResultChanged, 0, Array<byte>.Empty, Array<byte>.Empty, Array<UnresolvedPropertyDefinition>.Empty, Array<object>.Empty);
		}

		// Token: 0x06004770 RID: 18288 RVA: 0x0012FB60 File Offset: 0x0012DD60
		public QueryNotification CreateRowAddedNotification()
		{
			return new QueryNotification(QueryNotificationType.RowAdded, this.errorCode, this.index, this.prior, this.propertyDefinitions, this.row);
		}

		// Token: 0x0400270F RID: 9999
		private readonly ICollection<PropertyDefinition> propertyDefinitions;

		// Token: 0x04002710 RID: 10000
		private readonly QueryNotificationType eventType;

		// Token: 0x04002711 RID: 10001
		private readonly int errorCode;

		// Token: 0x04002712 RID: 10002
		private readonly byte[] index;

		// Token: 0x04002713 RID: 10003
		private readonly byte[] prior;

		// Token: 0x04002714 RID: 10004
		private readonly object[] row;
	}
}
