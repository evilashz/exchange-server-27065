using System;
using System.Threading;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000581 RID: 1409
	internal class CreateTestItemContext
	{
		// Token: 0x06003191 RID: 12689 RVA: 0x000C98CE File Offset: 0x000C7ACE
		public CreateTestItemContext(ExchangePrincipal exchangePrincipal, int sleepTime)
		{
			this.exchangePrincipal = exchangePrincipal;
			this.sleepTime = sleepTime;
			this.createTestItemEvent = new EventWaitHandle(false, EventResetMode.ManualReset);
		}

		// Token: 0x17000EAC RID: 3756
		// (get) Token: 0x06003192 RID: 12690 RVA: 0x000C98F1 File Offset: 0x000C7AF1
		public EventWaitHandle CreateTestItemEvent
		{
			get
			{
				return this.createTestItemEvent;
			}
		}

		// Token: 0x17000EAD RID: 3757
		// (get) Token: 0x06003193 RID: 12691 RVA: 0x000C98F9 File Offset: 0x000C7AF9
		public ExchangePrincipal ExchangePrincipal
		{
			get
			{
				return this.exchangePrincipal;
			}
		}

		// Token: 0x17000EAE RID: 3758
		// (get) Token: 0x06003194 RID: 12692 RVA: 0x000C9901 File Offset: 0x000C7B01
		public int SleepTime
		{
			get
			{
				return this.sleepTime;
			}
		}

		// Token: 0x17000EAF RID: 3759
		// (get) Token: 0x06003195 RID: 12693 RVA: 0x000C9909 File Offset: 0x000C7B09
		// (set) Token: 0x06003196 RID: 12694 RVA: 0x000C9911 File Offset: 0x000C7B11
		public VersionedId TestItemId
		{
			get
			{
				return this.testItemId;
			}
			set
			{
				this.testItemId = value;
			}
		}

		// Token: 0x17000EB0 RID: 3760
		// (get) Token: 0x06003197 RID: 12695 RVA: 0x000C991A File Offset: 0x000C7B1A
		// (set) Token: 0x06003198 RID: 12696 RVA: 0x000C9922 File Offset: 0x000C7B22
		public LocalizedException LocalizedException
		{
			get
			{
				return this.localizedException;
			}
			set
			{
				this.localizedException = value;
			}
		}

		// Token: 0x04002327 RID: 8999
		private ExchangePrincipal exchangePrincipal;

		// Token: 0x04002328 RID: 9000
		private readonly int sleepTime;

		// Token: 0x04002329 RID: 9001
		private VersionedId testItemId;

		// Token: 0x0400232A RID: 9002
		private LocalizedException localizedException;

		// Token: 0x0400232B RID: 9003
		private EventWaitHandle createTestItemEvent;
	}
}
