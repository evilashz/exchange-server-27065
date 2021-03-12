using System;
using System.Collections.Generic;
using System.DirectoryServices.Protocols;

namespace Microsoft.Exchange.Data.Directory.DirSync
{
	// Token: 0x020001B4 RID: 436
	internal class ADDirSyncReader<TResult> : ADGenericPagedReader<TResult> where TResult : ADDirSyncResult, new()
	{
		// Token: 0x0600121F RID: 4639 RVA: 0x00057E2C File Offset: 0x0005602C
		internal ADDirSyncReader(IDirectorySession session, QueryFilter filter, IEnumerable<PropertyDefinition> properties) : this(session, filter, 100, properties)
		{
		}

		// Token: 0x06001220 RID: 4640 RVA: 0x00057E3C File Offset: 0x0005603C
		internal ADDirSyncReader(IDirectorySession session, QueryFilter filter, int pageSize, IEnumerable<PropertyDefinition> properties) : base(session, ADDirSyncReader<TResult>.GetSearchRootForSession(session), QueryScope.SubTree, filter, null, pageSize, properties, false)
		{
			this.dirSyncRequestControl = new DirSyncRequestControl();
			this.dirSyncRequestControl.Option = (DirectorySynchronizationOptions)((ulong)int.MinValue);
			base.DirectoryControls.Add(this.dirSyncRequestControl);
		}

		// Token: 0x06001221 RID: 4641 RVA: 0x00057E8B File Offset: 0x0005608B
		internal TResult[] GetNextResultPage()
		{
			return this.GetNextPage();
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06001222 RID: 4642 RVA: 0x00057E93 File Offset: 0x00056093
		protected override int SizeLimit
		{
			get
			{
				return base.PageSize;
			}
		}

		// Token: 0x06001223 RID: 4643 RVA: 0x00057E9C File Offset: 0x0005609C
		protected override SearchResultEntryCollection GetNextResultCollection()
		{
			this.dirSyncRequestControl.Cookie = base.Cookie;
			DirectoryControl directoryControl;
			SearchResultEntryCollection nextResultCollection = base.GetNextResultCollection(typeof(DirSyncResponseControl), out directoryControl);
			DirSyncResponseControl dirSyncResponseControl = directoryControl as DirSyncResponseControl;
			base.Cookie = dirSyncResponseControl.Cookie;
			base.RetrievedAllData = new bool?(!dirSyncResponseControl.MoreData);
			return nextResultCollection;
		}

		// Token: 0x06001224 RID: 4644 RVA: 0x00057EF5 File Offset: 0x000560F5
		private static ADObjectId GetSearchRootForSession(IDirectorySession session)
		{
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			if (!session.UseConfigNC)
			{
				return session.GetDomainNamingContext();
			}
			return session.GetConfigurationNamingContext();
		}

		// Token: 0x04000A86 RID: 2694
		private const int DirSyncDefaultSizeLimit = 100;

		// Token: 0x04000A87 RID: 2695
		private readonly DirSyncRequestControl dirSyncRequestControl;
	}
}
