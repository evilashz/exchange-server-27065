using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.ProvisioningAgent
{
	// Token: 0x02000004 RID: 4
	public class AdminAuditLogHealth
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000015 RID: 21 RVA: 0x0000240C File Offset: 0x0000060C
		// (set) Token: 0x06000016 RID: 22 RVA: 0x000024AC File Offset: 0x000006AC
		public ExceptionDetails[] Exceptions
		{
			get
			{
				ExceptionDetails[] result;
				lock (this.syncRoot)
				{
					ExceptionDetails[] array = new ExceptionDetails[this.exceptions.Count];
					int num = 0;
					foreach (Exception exception in this.exceptions)
					{
						array[num++] = ExceptionDetails.FromException(exception);
					}
					result = array;
				}
				return result;
			}
			set
			{
				throw new NotSupportedException("AdminAuditLogHealth.Exceptions is read-only");
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000024B8 File Offset: 0x000006B8
		public void AddException(Exception exception)
		{
			lock (this.syncRoot)
			{
				if (this.exceptions.Count >= 25)
				{
					this.exceptions.Dequeue();
				}
				this.exceptions.Enqueue(exception);
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x0000251C File Offset: 0x0000071C
		public void Clear()
		{
			lock (this.syncRoot)
			{
				this.exceptions.Clear();
			}
		}

		// Token: 0x04000015 RID: 21
		internal const int MaxExceptions = 25;

		// Token: 0x04000016 RID: 22
		private readonly object syncRoot = new object();

		// Token: 0x04000017 RID: 23
		private readonly Queue<Exception> exceptions = new Queue<Exception>();
	}
}
