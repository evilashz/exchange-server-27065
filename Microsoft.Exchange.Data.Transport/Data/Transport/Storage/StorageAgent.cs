using System;

namespace Microsoft.Exchange.Data.Transport.Storage
{
	// Token: 0x02000091 RID: 145
	internal abstract class StorageAgent : Agent
	{
		// Token: 0x1400001C RID: 28
		// (add) Token: 0x06000363 RID: 867 RVA: 0x0000869D File Offset: 0x0000689D
		// (remove) Token: 0x06000364 RID: 868 RVA: 0x000086AB File Offset: 0x000068AB
		protected event LoadedMessageEventHandler OnLoadedMessage
		{
			add
			{
				base.AddHandler("OnLoadedMessage", value);
			}
			remove
			{
				base.RemoveHandler("OnLoadedMessage");
			}
		}

		// Token: 0x06000365 RID: 869 RVA: 0x000086B8 File Offset: 0x000068B8
		internal override void Invoke(string eventTopic, object source, object e)
		{
			Delegate @delegate = (Delegate)base.Handlers[eventTopic];
			if (@delegate == null)
			{
				return;
			}
			if (eventTopic != null)
			{
				if (!(eventTopic == "OnLoadedMessage"))
				{
					return;
				}
				((LoadedMessageEventHandler)@delegate)((StorageEventSource)source, (StorageEventArgs)e);
			}
		}
	}
}
