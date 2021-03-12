using System;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000291 RID: 657
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class ObjectThreadTracker
	{
		// Token: 0x1700088B RID: 2187
		// (get) Token: 0x06001B58 RID: 7000 RVA: 0x0007E386 File Offset: 0x0007C586
		internal int OwningThreadId
		{
			get
			{
				return this.owningThreadId;
			}
		}

		// Token: 0x06001B59 RID: 7001 RVA: 0x0007E38E File Offset: 0x0007C58E
		internal ObjectThreadTracker()
		{
			this.lockObject = new object();
			this.owningThreadId = -1;
			this.lastMethodName = string.Empty;
			this.numberOfOwnerReferences = 0;
		}

		// Token: 0x06001B5A RID: 7002 RVA: 0x0007E3BC File Offset: 0x0007C5BC
		internal void Enter(string methodName)
		{
			int currentManagedThreadId = Environment.CurrentManagedThreadId;
			lock (this.lockObject)
			{
				if (this.owningThreadId != -1 && this.owningThreadId != currentManagedThreadId)
				{
					throw new InvalidOperationException(string.Format("In call to {0}, storeSession object is already being used by thread ID:{1} at {2}.", methodName, currentManagedThreadId, this.lastMethodName));
				}
				this.owningThreadId = currentManagedThreadId;
				this.lastMethodName = methodName;
				this.numberOfOwnerReferences++;
			}
		}

		// Token: 0x06001B5B RID: 7003 RVA: 0x0007E448 File Offset: 0x0007C648
		internal void Exit()
		{
			lock (this.lockObject)
			{
				if (this.numberOfOwnerReferences > 0 && Environment.CurrentManagedThreadId == this.owningThreadId)
				{
					this.numberOfOwnerReferences--;
					if (this.numberOfOwnerReferences == 0)
					{
						this.owningThreadId = -1;
					}
				}
				else
				{
					string arg = (this.numberOfOwnerReferences == 0) ? "the calling thread doesn't own this tracker" : "no threads have entered this tracker";
					string message = string.Format("Attempting to Exit() while in an invalid state - {0}. This cannot be recovered from and is fatal.", arg);
					ExDiagnostics.FailFast(message, true);
				}
			}
		}

		// Token: 0x040012FD RID: 4861
		private int owningThreadId;

		// Token: 0x040012FE RID: 4862
		private string lastMethodName;

		// Token: 0x040012FF RID: 4863
		private object lockObject;

		// Token: 0x04001300 RID: 4864
		private int numberOfOwnerReferences;
	}
}
