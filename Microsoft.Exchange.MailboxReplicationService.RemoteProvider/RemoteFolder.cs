using System;
using System.Collections.Generic;
using System.Security.AccessControl;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000005 RID: 5
	internal abstract class RemoteFolder : RemoteObject, IFolder, IDisposable
	{
		// Token: 0x0600000F RID: 15 RVA: 0x00002325 File Offset: 0x00000525
		protected RemoteFolder(IMailboxReplicationProxyService mrsProxy, long handle, byte[] folderId, RemoteMailbox mailbox) : base(mrsProxy, handle)
		{
			this.folderId = folderId;
			this.mailbox = mailbox;
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000010 RID: 16 RVA: 0x0000233E File Offset: 0x0000053E
		public byte[] FolderId
		{
			get
			{
				return this.folderId;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002346 File Offset: 0x00000546
		public RemoteMailbox Mailbox
		{
			get
			{
				return this.mailbox;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000012 RID: 18 RVA: 0x0000234E File Offset: 0x0000054E
		// (set) Token: 0x06000013 RID: 19 RVA: 0x00002356 File Offset: 0x00000556
		private protected string FolderName { protected get; private set; }

		// Token: 0x06000014 RID: 20 RVA: 0x00002360 File Offset: 0x00000560
		byte[] IFolder.GetFolderId()
		{
			MrsTracer.ProxyClient.Function("IFolder.GetFolderId(): {0}", new object[]
			{
				this.FolderName
			});
			return this.FolderId;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002394 File Offset: 0x00000594
		List<MessageRec> IFolder.EnumerateMessages(EnumerateMessagesFlags emFlags, PropTag[] additionalPtagsToLoad)
		{
			MrsTracer.ProxyClient.Function("IFolder.EnumerateMessages(): {0}", new object[]
			{
				this.FolderName
			});
			bool flag;
			List<MessageRec> list = base.MrsProxy.IFolder_EnumerateMessagesPaged2(base.Handle, emFlags, DataConverter<PropTagConverter, PropTag, int>.GetData(additionalPtagsToLoad), out flag);
			while (flag)
			{
				List<MessageRec> collection = base.MrsProxy.IFolder_EnumerateMessagesNextBatch(base.Handle, out flag);
				list.AddRange(collection);
			}
			foreach (MessageRec messageRec in list)
			{
				messageRec.FolderId = this.folderId;
			}
			MrsTracer.ProxyClient.Debug("IFolder.EnumerateMessages(): {0} returned {1} messages", new object[]
			{
				this.FolderName,
				list.Count
			});
			return list;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x0000247C File Offset: 0x0000067C
		FolderRec IFolder.GetFolderRec(PropTag[] additionalPtagsToLoad, GetFolderRecFlags flags)
		{
			if (!base.ServerVersion[15])
			{
				return base.MrsProxy.IFolder_GetFolderRec2(base.Handle, DataConverter<PropTagConverter, PropTag, int>.GetData(additionalPtagsToLoad));
			}
			FolderRec folderRec = base.MrsProxy.IFolder_GetFolderRec3(base.Handle, DataConverter<PropTagConverter, PropTag, int>.GetData(additionalPtagsToLoad), (int)flags);
			this.FolderName = folderRec.FolderName;
			MrsTracer.ProxyClient.Function("IFolder.GetFolderRec(): {0}", new object[]
			{
				this.FolderName
			});
			return folderRec;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000024F8 File Offset: 0x000006F8
		RawSecurityDescriptor IFolder.GetSecurityDescriptor(SecurityProp secProp)
		{
			MrsTracer.ProxyClient.Function("IFolder.GetSecurityDescriptor(): {0}", new object[]
			{
				this.FolderName
			});
			byte[] array = base.MrsProxy.IFolder_GetSecurityDescriptor(base.Handle, (int)secProp);
			if (array == null)
			{
				return null;
			}
			return new RawSecurityDescriptor(array, 0);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002544 File Offset: 0x00000744
		void IFolder.SetContentsRestriction(RestrictionData restriction)
		{
			MrsTracer.ProxyClient.Function("IFolder.SetContentsRestriction(): {0}", new object[]
			{
				this.FolderName
			});
			if (!base.ServerVersion[8])
			{
				throw new UnsupportedRemoteServerVersionWithOperationPermanentException(base.MrsProxyClient.ServerName, base.ServerVersion.ToString(), "IFolder_SetContentsRestriction");
			}
			base.MrsProxy.IFolder_SetContentsRestriction(base.Handle, restriction);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000025B4 File Offset: 0x000007B4
		PropValueData[] IFolder.GetProps(PropTag[] pta)
		{
			MrsTracer.ProxyClient.Function("IFolder.GetProps(): {0}", new object[]
			{
				this.FolderName
			});
			if (base.ServerVersion[8])
			{
				return base.MrsProxy.IFolder_GetProps(base.Handle, DataConverter<PropTagConverter, PropTag, int>.GetData(pta));
			}
			if (this is RemoteSourceFolder)
			{
				return base.MrsProxy.ISourceFolder_GetProps(base.Handle, DataConverter<PropTagConverter, PropTag, int>.GetData(pta));
			}
			throw new UnsupportedRemoteServerVersionWithOperationPermanentException(base.MrsProxyClient.ServerName, base.ServerVersion.ToString(), "IFolder_GetProps");
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002648 File Offset: 0x00000848
		void IFolder.GetSearchCriteria(out RestrictionData restriction, out byte[][] entryIds, out SearchState state)
		{
			MrsTracer.ProxyClient.Function("IFolder.GetSearchCriteria(): {0}", new object[]
			{
				this.FolderName
			});
			if (base.ServerVersion[8])
			{
				int num;
				base.MrsProxy.IFolder_GetSearchCriteria(base.Handle, out restriction, out entryIds, out num);
				state = (SearchState)num;
				return;
			}
			if (this is RemoteSourceFolder)
			{
				int num;
				base.MrsProxy.ISourceFolder_GetSearchCriteria(base.Handle, out restriction, out entryIds, out num);
				state = (SearchState)num;
				return;
			}
			throw new UnsupportedRemoteServerVersionWithOperationPermanentException(base.MrsProxyClient.ServerName, base.ServerVersion.ToString(), "IFolder_GetProps");
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002708 File Offset: 0x00000908
		void IFolder.DeleteMessages(byte[][] entryIds)
		{
			MrsTracer.ProxyClient.Function("IFolder.DeleteMessages(): {0}", new object[]
			{
				this.FolderName
			});
			if (base.ServerVersion[8])
			{
				CommonUtils.ProcessInBatches<byte[]>(entryIds, 1000, delegate(byte[][] batch)
				{
					base.MrsProxy.IFolder_DeleteMessages(base.Handle, batch);
				});
				return;
			}
			if (this is RemoteDestinationFolder)
			{
				CommonUtils.ProcessInBatches<byte[]>(entryIds, 1000, delegate(byte[][] batch)
				{
					base.MrsProxy.IDestinationFolder_DeleteMessages(base.Handle, batch);
				});
				return;
			}
			throw new UnsupportedRemoteServerVersionWithOperationPermanentException(base.MrsProxyClient.ServerName, base.ServerVersion.ToString(), "IFolder_DeleteMessages");
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000027A4 File Offset: 0x000009A4
		RuleData[] IFolder.GetRules(PropTag[] extraProps)
		{
			MrsTracer.ProxyClient.Function("IFolder.GetRules(): {0}", new object[]
			{
				this.FolderName
			});
			if (!base.ServerVersion[8])
			{
				throw new UnsupportedRemoteServerVersionWithOperationPermanentException(base.MrsProxyClient.ServerName, base.ServerVersion.ToString(), "IFolder_GetRules");
			}
			return base.MrsProxy.IFolder_GetRules(base.Handle, DataConverter<PropTagConverter, PropTag, int>.GetData(extraProps));
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002818 File Offset: 0x00000A18
		PropValueData[][] IFolder.GetACL(SecurityProp secProp)
		{
			MrsTracer.ProxyClient.Function("IFolder.GetACL(): {0}", new object[]
			{
				this.FolderName
			});
			if (!base.ServerVersion[8])
			{
				throw new UnsupportedRemoteServerVersionWithOperationPermanentException(base.MrsProxyClient.ServerName, base.ServerVersion.ToString(), "IFolder_GetACL");
			}
			return base.MrsProxy.IFolder_GetACL(base.Handle, (int)secProp);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002888 File Offset: 0x00000A88
		PropValueData[][] IFolder.GetExtendedAcl(AclFlags aclFlags)
		{
			MrsTracer.ProxyClient.Function("IFolder.GetExtendedAcl(): {0}", new object[]
			{
				this.FolderName
			});
			if (!base.ServerVersion[51])
			{
				throw new UnsupportedRemoteServerVersionWithOperationPermanentException(base.MrsProxyClient.ServerName, base.ServerVersion.ToString(), "IFolder_GetExtendedAcl");
			}
			return base.MrsProxy.IFolder_GetExtendedAcl(base.Handle, (int)aclFlags);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000028F8 File Offset: 0x00000AF8
		List<MessageRec> IFolder.LookupMessages(PropTag ptagToLookup, List<byte[]> keysToLookup, PropTag[] additionalPtagsToLoad)
		{
			MrsTracer.ProxyClient.Function("IFolder.LookupMessages(): {0}", new object[]
			{
				this.FolderName
			});
			if (!base.ServerVersion[16])
			{
				throw new UnsupportedRemoteServerVersionWithOperationPermanentException(base.MrsProxyClient.ServerName, base.ServerVersion.ToString(), "IFolder_LookupMessages");
			}
			return base.MrsProxy.IFolder_LookupMessages(base.Handle, DataConverter<PropTagConverter, PropTag, int>.GetData(ptagToLookup), keysToLookup.ToArray(), DataConverter<PropTagConverter, PropTag, int>.GetData(additionalPtagsToLoad));
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002978 File Offset: 0x00000B78
		PropProblemData[] IFolder.SetProps(PropValueData[] pva)
		{
			MrsTracer.ProxyClient.Function("IFolder.SetProps(): {0}", new object[]
			{
				this.FolderName
			});
			return base.MrsProxy.IFolder_SetProps(base.Handle, pva);
		}

		// Token: 0x04000004 RID: 4
		protected const int BatchSize = 1000;

		// Token: 0x04000005 RID: 5
		private byte[] folderId;

		// Token: 0x04000006 RID: 6
		private RemoteMailbox mailbox;
	}
}
