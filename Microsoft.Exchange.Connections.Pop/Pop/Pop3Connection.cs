using System;
using System.Collections.Generic;
using Microsoft.Exchange.Connections.Common;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Pop
{
	// Token: 0x0200000B RID: 11
	[ClassAccessLevel(AccessLevel.Implementation)]
	public sealed class Pop3Connection : DisposeTrackableBase, IPop3Connection, IConnection<IPop3Connection>, IDisposable
	{
		// Token: 0x06000067 RID: 103 RVA: 0x00004ECD File Offset: 0x000030CD
		private Pop3Connection(ConnectionParameters connectionParameters)
		{
			this.connectionParameters = connectionParameters;
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00004EDC File Offset: 0x000030DC
		public Pop3ConnectionContext ConnectionContext
		{
			get
			{
				base.CheckDisposed();
				return this.connectionContext;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00004EEA File Offset: 0x000030EA
		private ConnectionParameters ConnectionParameters
		{
			get
			{
				base.CheckDisposed();
				return this.connectionParameters;
			}
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00004EF8 File Offset: 0x000030F8
		public static IPop3Connection CreateInstance(ConnectionParameters connectionParameters)
		{
			return Pop3Connection.hookableFactory.Value(connectionParameters).Initialize();
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00004F0F File Offset: 0x0000310F
		public IPop3Connection Initialize()
		{
			this.connectionContext = new Pop3ConnectionContext(this.ConnectionParameters, null);
			return this;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00004F24 File Offset: 0x00003124
		public void ConnectAndAuthenticate(Pop3ServerParameters serverParameters, Pop3AuthenticationParameters authenticationParameters)
		{
			base.CheckDisposed();
			Pop3ConnectionContext pop3ConnectionContext = this.ConnectionContext;
			pop3ConnectionContext.AuthenticationParameters = authenticationParameters;
			pop3ConnectionContext.ServerParameters = serverParameters;
			pop3ConnectionContext.Client = new Pop3Client(serverParameters, authenticationParameters, this.connectionParameters, null, null);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00004F64 File Offset: 0x00003164
		public Pop3ResultData GetUniqueIds()
		{
			base.CheckDisposed();
			AsyncOperationResult<Pop3ResultData> uniqueIds = Pop3ConnectionCore.GetUniqueIds(this.ConnectionContext, null, null);
			this.ThrowIfExceptionNotNull(uniqueIds.Exception);
			return uniqueIds.Data;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00004F98 File Offset: 0x00003198
		public Pop3ResultData DeleteEmails(List<int> messageIds)
		{
			base.CheckDisposed();
			AsyncOperationResult<Pop3ResultData> asyncOperationResult = Pop3ConnectionCore.DeleteEmails(this.ConnectionContext, messageIds, null, null);
			this.ThrowIfExceptionNotNull(asyncOperationResult.Exception);
			return asyncOperationResult.Data;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00004FCC File Offset: 0x000031CC
		public Pop3ResultData GetEmail(int messageId)
		{
			base.CheckDisposed();
			AsyncOperationResult<Pop3ResultData> email = Pop3ConnectionCore.GetEmail(this.ConnectionContext, messageId, null, null);
			this.ThrowIfExceptionNotNull(email.Exception);
			return email.Data;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00005000 File Offset: 0x00003200
		public Pop3ResultData Quit()
		{
			base.CheckDisposed();
			AsyncOperationResult<Pop3ResultData> asyncOperationResult = Pop3ConnectionCore.Quit(this.ConnectionContext, null, null);
			this.ThrowIfExceptionNotNull(asyncOperationResult.Exception);
			return asyncOperationResult.Data;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00005034 File Offset: 0x00003234
		public Pop3ResultData VerifyAccount()
		{
			base.CheckDisposed();
			AsyncOperationResult<Pop3ResultData> asyncOperationResult = Pop3ConnectionCore.VerifyAccount(this.ConnectionContext, null, null);
			this.ThrowIfExceptionNotNull(asyncOperationResult.Exception);
			return asyncOperationResult.Data;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00005067 File Offset: 0x00003267
		internal static IDisposable SetTestHook(Func<ConnectionParameters, IPop3Connection> newFactory)
		{
			return Pop3Connection.hookableFactory.SetTestHook(newFactory);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00005074 File Offset: 0x00003274
		protected override void InternalDispose(bool disposing)
		{
			if (disposing && this.ConnectionContext != null)
			{
				this.ConnectionContext.Dispose();
				this.connectionContext = null;
			}
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00005093 File Offset: 0x00003293
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<Pop3Connection>(this);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x0000509B File Offset: 0x0000329B
		private static IPop3Connection Factory(ConnectionParameters connectionParameters)
		{
			return new Pop3Connection(connectionParameters).Initialize();
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000050A8 File Offset: 0x000032A8
		private void ThrowIfExceptionNotNull(Exception exceptionOrNull)
		{
			if (exceptionOrNull == null)
			{
				return;
			}
			if (exceptionOrNull is LocalizedException)
			{
				throw exceptionOrNull;
			}
			string fullName = exceptionOrNull.GetType().FullName;
			throw new UnhandledException(fullName, exceptionOrNull);
		}

		// Token: 0x0400004D RID: 77
		private static Hookable<Func<ConnectionParameters, IPop3Connection>> hookableFactory = Hookable<Func<ConnectionParameters, IPop3Connection>>.Create(true, new Func<ConnectionParameters, IPop3Connection>(Pop3Connection.Factory));

		// Token: 0x0400004E RID: 78
		private Pop3ConnectionContext connectionContext;

		// Token: 0x0400004F RID: 79
		private ConnectionParameters connectionParameters;
	}
}
