using System;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.AnchorService
{
	// Token: 0x02000002 RID: 2
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class SimpleContext : DisposeTrackableBase
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public SimpleContext(string applicationName) : this(applicationName, new AnchorConfig(applicationName))
		{
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020DF File Offset: 0x000002DF
		internal SimpleContext(string applicationName, AnchorConfig config)
		{
			this.Initialize(applicationName, this.CreateLogger(applicationName, config), config);
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000020F7 File Offset: 0x000002F7
		protected SimpleContext()
		{
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000004 RID: 4 RVA: 0x000020FF File Offset: 0x000002FF
		// (set) Token: 0x06000005 RID: 5 RVA: 0x00002107 File Offset: 0x00000307
		public string ApplicationName
		{
			get
			{
				return this.applicationName;
			}
			protected set
			{
				this.applicationName = SimpleContext.RemoveWhiteSpace.Replace(value, string.Empty);
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000006 RID: 6 RVA: 0x0000211F File Offset: 0x0000031F
		// (set) Token: 0x06000007 RID: 7 RVA: 0x00002127 File Offset: 0x00000327
		public AnchorConfig Config { get; private set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002130 File Offset: 0x00000330
		// (set) Token: 0x06000009 RID: 9 RVA: 0x00002138 File Offset: 0x00000338
		public ILogger Logger { get; private set; }

		// Token: 0x0600000A RID: 10 RVA: 0x00002141 File Offset: 0x00000341
		public override void SuppressDisposeTracker()
		{
			if (this.Logger != null)
			{
				this.Logger.SuppressDisposeTracker();
			}
			base.SuppressDisposeTracker();
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000215C File Offset: 0x0000035C
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<SimpleContext>(this);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002164 File Offset: 0x00000364
		protected override void InternalDispose(bool disposing)
		{
			if (disposing && this.Logger != null)
			{
				this.Logger.Dispose();
				this.Logger = null;
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002183 File Offset: 0x00000383
		protected void Initialize(string applicationName, ILogger logger, AnchorConfig config)
		{
			AnchorUtil.ThrowOnNullOrEmptyArgument(applicationName, "applicationName");
			this.ApplicationName = applicationName;
			this.Logger = logger;
			this.Config = config;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000021A5 File Offset: 0x000003A5
		protected virtual ILogger CreateLogger(string applicationName, AnchorConfig config)
		{
			return new AnchorLogger(applicationName, config);
		}

		// Token: 0x04000001 RID: 1
		private static readonly Regex RemoveWhiteSpace = new Regex("\\s+");

		// Token: 0x04000002 RID: 2
		private string applicationName;
	}
}
