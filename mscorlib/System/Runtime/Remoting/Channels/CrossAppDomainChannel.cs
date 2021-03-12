using System;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using System.Security;
using System.Security.Permissions;
using System.Threading;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x02000809 RID: 2057
	[Serializable]
	internal class CrossAppDomainChannel : IChannel, IChannelSender, IChannelReceiver
	{
		// Token: 0x17000ECC RID: 3788
		// (get) Token: 0x060058B4 RID: 22708 RVA: 0x001380C1 File Offset: 0x001362C1
		// (set) Token: 0x060058B5 RID: 22709 RVA: 0x001380D7 File Offset: 0x001362D7
		private static CrossAppDomainChannel gAppDomainChannel
		{
			get
			{
				return Thread.GetDomain().RemotingData.ChannelServicesData.xadmessageSink;
			}
			set
			{
				Thread.GetDomain().RemotingData.ChannelServicesData.xadmessageSink = value;
			}
		}

		// Token: 0x17000ECD RID: 3789
		// (get) Token: 0x060058B6 RID: 22710 RVA: 0x001380F0 File Offset: 0x001362F0
		internal static CrossAppDomainChannel AppDomainChannel
		{
			get
			{
				if (CrossAppDomainChannel.gAppDomainChannel == null)
				{
					CrossAppDomainChannel gAppDomainChannel = new CrossAppDomainChannel();
					object obj = CrossAppDomainChannel.staticSyncObject;
					lock (obj)
					{
						if (CrossAppDomainChannel.gAppDomainChannel == null)
						{
							CrossAppDomainChannel.gAppDomainChannel = gAppDomainChannel;
						}
					}
				}
				return CrossAppDomainChannel.gAppDomainChannel;
			}
		}

		// Token: 0x060058B7 RID: 22711 RVA: 0x00138148 File Offset: 0x00136348
		[SecurityCritical]
		internal static void RegisterChannel()
		{
			CrossAppDomainChannel appDomainChannel = CrossAppDomainChannel.AppDomainChannel;
			ChannelServices.RegisterChannelInternal(appDomainChannel, false);
		}

		// Token: 0x17000ECE RID: 3790
		// (get) Token: 0x060058B8 RID: 22712 RVA: 0x00138162 File Offset: 0x00136362
		public virtual string ChannelName
		{
			[SecurityCritical]
			get
			{
				return "XAPPDMN";
			}
		}

		// Token: 0x17000ECF RID: 3791
		// (get) Token: 0x060058B9 RID: 22713 RVA: 0x00138169 File Offset: 0x00136369
		public virtual string ChannelURI
		{
			get
			{
				return "XAPPDMN_URI";
			}
		}

		// Token: 0x17000ED0 RID: 3792
		// (get) Token: 0x060058BA RID: 22714 RVA: 0x00138170 File Offset: 0x00136370
		public virtual int ChannelPriority
		{
			[SecurityCritical]
			get
			{
				return 100;
			}
		}

		// Token: 0x060058BB RID: 22715 RVA: 0x00138174 File Offset: 0x00136374
		[SecurityCritical]
		public string Parse(string url, out string objectURI)
		{
			objectURI = url;
			return null;
		}

		// Token: 0x17000ED1 RID: 3793
		// (get) Token: 0x060058BC RID: 22716 RVA: 0x0013817A File Offset: 0x0013637A
		public virtual object ChannelData
		{
			[SecurityCritical]
			get
			{
				return new CrossAppDomainData(Context.DefaultContext.InternalContextID, Thread.GetDomain().GetId(), Identity.ProcessGuid);
			}
		}

		// Token: 0x060058BD RID: 22717 RVA: 0x0013819C File Offset: 0x0013639C
		[SecurityCritical]
		public virtual IMessageSink CreateMessageSink(string url, object data, out string objectURI)
		{
			objectURI = null;
			IMessageSink result = null;
			if (url != null && data == null)
			{
				if (url.StartsWith("XAPPDMN", StringComparison.Ordinal))
				{
					throw new RemotingException(Environment.GetResourceString("Remoting_AppDomains_NYI"));
				}
			}
			else
			{
				CrossAppDomainData crossAppDomainData = data as CrossAppDomainData;
				if (crossAppDomainData != null && crossAppDomainData.ProcessGuid.Equals(Identity.ProcessGuid))
				{
					result = CrossAppDomainSink.FindOrCreateSink(crossAppDomainData);
				}
			}
			return result;
		}

		// Token: 0x060058BE RID: 22718 RVA: 0x001381F6 File Offset: 0x001363F6
		[SecurityCritical]
		public virtual string[] GetUrlsForUri(string objectURI)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_Method"));
		}

		// Token: 0x060058BF RID: 22719 RVA: 0x00138207 File Offset: 0x00136407
		[SecurityCritical]
		public virtual void StartListening(object data)
		{
		}

		// Token: 0x060058C0 RID: 22720 RVA: 0x00138209 File Offset: 0x00136409
		[SecurityCritical]
		public virtual void StopListening(object data)
		{
		}

		// Token: 0x0400282A RID: 10282
		private const string _channelName = "XAPPDMN";

		// Token: 0x0400282B RID: 10283
		private const string _channelURI = "XAPPDMN_URI";

		// Token: 0x0400282C RID: 10284
		private static object staticSyncObject = new object();

		// Token: 0x0400282D RID: 10285
		private static PermissionSet s_fullTrust = new PermissionSet(PermissionState.Unrestricted);
	}
}
