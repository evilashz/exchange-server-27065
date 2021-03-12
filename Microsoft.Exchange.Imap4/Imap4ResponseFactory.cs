using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Data.ApplicationLogic;
using Microsoft.Exchange.Data.ApplicationLogic.Diagnostics;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.PopImap.Core;
using Microsoft.Exchange.Security;
using Microsoft.Exchange.Security.Authentication;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Imap4
{
	// Token: 0x02000011 RID: 17
	internal sealed class Imap4ResponseFactory : ResponseFactory
	{
		// Token: 0x0600009A RID: 154 RVA: 0x00004F54 File Offset: 0x00003154
		internal Imap4ResponseFactory(ProtocolSession session) : base(session)
		{
			base.ProtocolUser = new Imap4ProtocolUser(session);
			this.moveEnabled = VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).Imap.RfcMoveImap.Enabled;
			this.moveEnabledCafe = VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).Imap.RfcMoveImapCafe.Enabled;
			base.SkipAuthOnCafeEnabled = VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).Imap.SkipAuthOnCafe.Enabled;
			this.idEnabled = VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).Imap.RfcIDImap.Enabled;
			this.idEnabledCafe = VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).Imap.RfcIDImapCafe.Enabled;
			this.RefreshSearchFolderItemsEnabled = VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).Imap.RefreshSearchFolderItems.Enabled;
			this.AllowPlainTextConversionWithoutUsingSkeleton = VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).Imap.AllowPlainTextConversionWithoutUsingSkeleton.Enabled;
			this.ReloadMailboxBeforeGettingSubscriptionList = VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).Imap.ReloadMailboxBeforeGettingSubscriptionList.Enabled;
			this.DontReturnLastMessageForUInt32MaxValue = VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).Imap.DontReturnLastMessageForUInt32MaxValue.Enabled;
			base.UseSamAccountNameAsUsername = VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).Imap.UseSamAccountNameAsUsername.Enabled;
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600009B RID: 155 RVA: 0x000050F0 File Offset: 0x000032F0
		// (set) Token: 0x0600009C RID: 156 RVA: 0x000050F7 File Offset: 0x000032F7
		public static bool MoveEnableAllowed { get; set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600009D RID: 157 RVA: 0x000050FF File Offset: 0x000032FF
		public bool MoveEnabled
		{
			get
			{
				return this.moveEnabled && Imap4ResponseFactory.MoveEnableAllowed;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600009E RID: 158 RVA: 0x00005110 File Offset: 0x00003310
		// (set) Token: 0x0600009F RID: 159 RVA: 0x00005118 File Offset: 0x00003318
		public bool DontReturnLastMessageForUInt32MaxValue { get; set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x00005121 File Offset: 0x00003321
		public bool MoveEnabledCafe
		{
			get
			{
				return this.moveEnabledCafe && Imap4ResponseFactory.MoveEnableAllowed;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x00005132 File Offset: 0x00003332
		public bool IDEnabled
		{
			get
			{
				return this.idEnabled;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x0000513A File Offset: 0x0000333A
		public bool IDEnabledCafe
		{
			get
			{
				return this.idEnabledCafe;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x00005142 File Offset: 0x00003342
		// (set) Token: 0x060000A4 RID: 164 RVA: 0x0000514A File Offset: 0x0000334A
		public bool RefreshSearchFolderItemsEnabled { get; private set; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x00005153 File Offset: 0x00003353
		// (set) Token: 0x060000A6 RID: 166 RVA: 0x0000515B File Offset: 0x0000335B
		public bool AllowPlainTextConversionWithoutUsingSkeleton { get; private set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x00005164 File Offset: 0x00003364
		// (set) Token: 0x060000A8 RID: 168 RVA: 0x0000516C File Offset: 0x0000336C
		public bool ReloadMailboxBeforeGettingSubscriptionList { get; set; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x00005175 File Offset: 0x00003375
		public MapiNotificationManager NotificationManager
		{
			get
			{
				if (this.notificationManager == null && !base.Disposed)
				{
					this.notificationManager = new MapiNotificationManager(this);
				}
				return this.notificationManager;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000AA RID: 170 RVA: 0x00005199 File Offset: 0x00003399
		protected override string ClientStringForMailboxSession
		{
			get
			{
				return "Client=POP3/IMAP4;Protocol=IMAP4";
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000AB RID: 171 RVA: 0x000051A0 File Offset: 0x000033A0
		public override string FirstAuthenticateResponse
		{
			get
			{
				return "+";
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000AC RID: 172 RVA: 0x000051A7 File Offset: 0x000033A7
		public override bool IsAuthenticated
		{
			get
			{
				return this.sessionState == Imap4State.Authenticated || this.sessionState == Imap4State.Selected || this.sessionState == Imap4State.Idle;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000AD RID: 173 RVA: 0x000051C6 File Offset: 0x000033C6
		public override bool IsDisconnected
		{
			get
			{
				return this.sessionState == Imap4State.Disconnected;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000AE RID: 174 RVA: 0x000051D2 File Offset: 0x000033D2
		public override string TimeoutErrorString
		{
			get
			{
				return "* BYE Connection is closed. 13";
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000AF RID: 175 RVA: 0x000051D9 File Offset: 0x000033D9
		// (set) Token: 0x060000B0 RID: 176 RVA: 0x000051E1 File Offset: 0x000033E1
		public Imap4State SessionState
		{
			get
			{
				return this.sessionState;
			}
			set
			{
				this.sessionState = value;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x000051EA File Offset: 0x000033EA
		// (set) Token: 0x060000B2 RID: 178 RVA: 0x000051F2 File Offset: 0x000033F2
		public Imap4State SavedSessionState
		{
			get
			{
				return this.savedSessionState;
			}
			set
			{
				this.savedSessionState = value;
			}
		}

		// Token: 0x1700004A RID: 74
		// (set) Token: 0x060000B3 RID: 179 RVA: 0x000051FB File Offset: 0x000033FB
		public string SavedIdleTag
		{
			set
			{
				this.savedIdleTag = value;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x00005204 File Offset: 0x00003404
		public int MaxReceiveSize
		{
			get
			{
				if (!((Imap4ProtocolUser)base.ProtocolUser).MaxReceiveSize.IsUnlimited)
				{
					return (int)((Imap4ProtocolUser)base.ProtocolUser).MaxReceiveSize.Value.ToBytes();
				}
				return ((Imap4Server)base.Session.Server).MaxReceiveSize;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x00005262 File Offset: 0x00003462
		// (set) Token: 0x060000B6 RID: 182 RVA: 0x0000526C File Offset: 0x0000346C
		public Imap4Mailbox SelectedMailbox
		{
			get
			{
				return this.selectedMailbox;
			}
			set
			{
				if (value != null)
				{
					ProtocolBaseServices.Assert(this.sessionState == Imap4State.Selected || this.sessionState == Imap4State.Authenticated, "Invalid session state!", new object[0]);
					if (!value.Equals(this.selectedMailbox))
					{
						if (this.selectedMailbox != null)
						{
							this.selectedMailbox.DisposeViews();
							this.folderHierarchyHandler.UnsubscribeObjectNotification();
						}
						this.selectedMailbox = value;
						this.folderHierarchyHandler.SubscribeObjectNotification(this.selectedMailbox.Uid);
						this.NotificationManager.RegisterHandler(this.selectedMailbox.DataAccessView);
						this.NotificationManager.RegisterHandler(this.selectedMailbox.FastQueryView);
						this.sessionState = Imap4State.Selected;
						return;
					}
					if (!value.ParentMailbox.Equals(this.selectedMailbox.ParentMailbox))
					{
						this.selectedMailbox.Name = value.Name;
						this.selectedMailbox.ParentMailbox = value.ParentMailbox;
						return;
					}
				}
				else
				{
					if (this.selectedMailbox != null)
					{
						this.selectedMailbox.DisposeViews();
						this.folderHierarchyHandler.UnsubscribeObjectNotification();
						this.selectedMailbox = null;
					}
					this.sessionState = Imap4State.Authenticated;
				}
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x00005388 File Offset: 0x00003588
		protected override ExEventLog.EventTuple NoDefaultAcceptedDomainFoundEventTuple
		{
			get
			{
				return Imap4EventLogConstants.Tuple_NoDefaultAcceptedDomainFound;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x0000538F File Offset: 0x0000358F
		protected override BudgetType BudgetType
		{
			get
			{
				return BudgetType.Imap;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x00005392 File Offset: 0x00003592
		public override string AuthenticationFailureString
		{
			get
			{
				return "AUTHENTICATE failed.";
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000BA RID: 186 RVA: 0x00005399 File Offset: 0x00003599
		protected override string AccountInvalidatedString
		{
			get
			{
				return "* BYE Session invalidated - {0}";
			}
		}

		// Token: 0x060000BB RID: 187 RVA: 0x000053A0 File Offset: 0x000035A0
		public override ProtocolRequest GenerateRequest(byte[] buf, int offset, int size)
		{
			int num;
			int nextToken = Imap4Session.GetNextToken(buf, offset, size, out num);
			if (nextToken != offset || size == 0)
			{
				if (base.Session.LightLogSession != null)
				{
					base.Session.LightLogSession.BeginCommand(Imap4ResponseFactory.InvalidBuf);
					base.Session.LightLogSession.Parameters = Encoding.ASCII.GetString(buf, offset, size);
				}
				return new Imap4RequestInvalid(this, "*", "Command Error. 12");
			}
			string text = null;
			int num2;
			if (this.sessionState == Imap4State.Idle && BaseSession.CompareArg(Imap4ResponseFactory.DoneBuf, buf, nextToken, num - nextToken))
			{
				num2 = size - (num - nextToken) - 1;
				if (num2 > 0)
				{
					text = Encoding.ASCII.GetString(buf, num + 1, num2);
				}
				if (base.Session.LightLogSession != null)
				{
					base.Session.LightLogSession.BeginCommand(Imap4ResponseFactory.DoneBuf);
					base.Session.LightLogSession.Parameters = text;
				}
				return new Imap4RequestDone(this, this.savedIdleTag, text);
			}
			string @string = Encoding.ASCII.GetString(buf, nextToken, num - nextToken);
			nextToken = Imap4Session.GetNextToken(buf, num, size - (num - offset), out num);
			if (nextToken != offset + @string.Length + 1)
			{
				if (base.Session.LightLogSession != null)
				{
					base.Session.LightLogSession.BeginCommand(Imap4ResponseFactory.InvalidBuf);
					base.Session.LightLogSession.Parameters = Encoding.ASCII.GetString(buf, offset, size);
				}
				return new Imap4RequestInvalid(this, @string, "Command Error. 12");
			}
			num2 = Math.Max(size - (num - offset) - 1, 0);
			if (BaseSession.CompareArg(Imap4ResponseFactory.LoginBuf, buf, nextToken, num - nextToken))
			{
				if (base.Session.LightLogSession != null)
				{
					base.Session.LightLogSession.BeginCommand(Imap4ResponseFactory.LoginBuf);
				}
				return new Imap4RequestLogin(this, @string, buf, num + 1, num2);
			}
			if (num2 > 0)
			{
				text = Encoding.ASCII.GetString(buf, num + 1, num2);
			}
			else
			{
				text = string.Empty;
			}
			ProtocolRequest protocolRequest = null;
			char c = (char)BaseSession.LowerC[(int)buf[nextToken]];
			byte[] array = null;
			switch (c)
			{
			case 'a':
				if (BaseSession.CompareArg(Imap4ResponseFactory.AppendBuf, buf, nextToken, num - nextToken))
				{
					array = Imap4ResponseFactory.AppendBuf;
					protocolRequest = new Imap4RequestAppend(this, @string, text);
				}
				else if (BaseSession.CompareArg(Imap4ResponseFactory.AuthenticateBuf, buf, nextToken, num - nextToken))
				{
					array = Imap4ResponseFactory.AuthenticateBuf;
					protocolRequest = new Imap4RequestAuthenticate(this, @string, text);
				}
				break;
			case 'c':
				if (BaseSession.CompareArg(Imap4ResponseFactory.CloseBuf, buf, nextToken, num - nextToken))
				{
					array = Imap4ResponseFactory.CloseBuf;
					protocolRequest = new Imap4RequestClose(this, @string, text);
				}
				else if (BaseSession.CompareArg(Imap4ResponseFactory.CapabilityBuf, buf, nextToken, num - nextToken))
				{
					array = Imap4ResponseFactory.CapabilityBuf;
					protocolRequest = new Imap4RequestCapability(this, @string, text);
				}
				else if (BaseSession.CompareArg(Imap4ResponseFactory.CopyBuf, buf, nextToken, num - nextToken))
				{
					array = Imap4ResponseFactory.CopyBuf;
					protocolRequest = new Imap4RequestCopy(this, @string, text);
				}
				else if (BaseSession.CompareArg(Imap4ResponseFactory.CreateBuf, buf, nextToken, num - nextToken))
				{
					array = Imap4ResponseFactory.CreateBuf;
					protocolRequest = new Imap4RequestCreate(this, @string, text);
				}
				else if (BaseSession.CompareArg(Imap4ResponseFactory.CheckBuf, buf, nextToken, num - nextToken))
				{
					array = Imap4ResponseFactory.CheckBuf;
					protocolRequest = new Imap4RequestCheck(this, @string, text);
				}
				break;
			case 'd':
				if (BaseSession.CompareArg(Imap4ResponseFactory.DeleteBuf, buf, nextToken, num - nextToken))
				{
					array = Imap4ResponseFactory.DeleteBuf;
					protocolRequest = new Imap4RequestDelete(this, @string, text);
				}
				break;
			case 'e':
				if (BaseSession.CompareArg(Imap4ResponseFactory.ExpungeBuf, buf, nextToken, num - nextToken))
				{
					array = Imap4ResponseFactory.ExpungeBuf;
					protocolRequest = new Imap4RequestExpunge(this, @string, text);
				}
				else if (BaseSession.CompareArg(Imap4ResponseFactory.ExamineBuf, buf, nextToken, num - nextToken))
				{
					array = Imap4ResponseFactory.ExamineBuf;
					protocolRequest = new Imap4RequestExamine(this, @string, text);
				}
				break;
			case 'f':
				if (BaseSession.CompareArg(Imap4ResponseFactory.FetchBuf, buf, nextToken, num - nextToken))
				{
					array = Imap4ResponseFactory.FetchBuf;
					protocolRequest = new Imap4RequestFetch(this, @string, text);
				}
				break;
			case 'i':
				if (BaseSession.CompareArg(Imap4ResponseFactory.IdleBuf, buf, nextToken, num - nextToken))
				{
					array = Imap4ResponseFactory.IdleBuf;
					protocolRequest = new Imap4RequestIdle(this, @string, text);
				}
				else if (BaseSession.CompareArg(Imap4ResponseFactory.IDBuf, buf, nextToken, num - nextToken) && ((this.IDEnabledCafe && ProtocolBaseServices.ServerRoleService == ServerServiceRole.cafe) || (this.IDEnabled && ProtocolBaseServices.ServerRoleService == ServerServiceRole.mailbox)))
				{
					array = Imap4ResponseFactory.IDBuf;
					protocolRequest = new Imap4RequestID(this, @string, text);
				}
				break;
			case 'l':
				if (BaseSession.CompareArg(Imap4ResponseFactory.ListBuf, buf, nextToken, num - nextToken))
				{
					array = Imap4ResponseFactory.ListBuf;
					protocolRequest = new Imap4RequestList(this, @string, text);
				}
				else if (BaseSession.CompareArg(Imap4ResponseFactory.LsubBuf, buf, nextToken, num - nextToken))
				{
					array = Imap4ResponseFactory.LsubBuf;
					protocolRequest = new Imap4RequestLsub(this, @string, text);
				}
				else if (BaseSession.CompareArg(Imap4ResponseFactory.LogoutBuf, buf, nextToken, num - nextToken))
				{
					array = Imap4ResponseFactory.LogoutBuf;
					protocolRequest = new Imap4RequestLogout(this, @string, text);
				}
				break;
			case 'm':
				if (BaseSession.CompareArg(Imap4ResponseFactory.MoveBuf, buf, nextToken, num - nextToken) && this.MoveEnabled)
				{
					array = Imap4ResponseFactory.MoveBuf;
					protocolRequest = new Imap4RequestMove(this, @string, text);
				}
				break;
			case 'n':
				if (BaseSession.CompareArg(Imap4ResponseFactory.NoopBuf, buf, nextToken, num - nextToken))
				{
					array = Imap4ResponseFactory.NoopBuf;
					protocolRequest = new Imap4RequestNoop(this, @string, text);
				}
				else if (BaseSession.CompareArg(Imap4ResponseFactory.NamespaceBuf, buf, nextToken, num - nextToken))
				{
					array = Imap4ResponseFactory.NamespaceBuf;
					protocolRequest = new Imap4RequestNamespace(this, @string, text);
				}
				break;
			case 'r':
				if (BaseSession.CompareArg(Imap4ResponseFactory.RenameBuf, buf, nextToken, num - nextToken))
				{
					array = Imap4ResponseFactory.RenameBuf;
					protocolRequest = new Imap4RequestRename(this, @string, text);
				}
				break;
			case 's':
				if (BaseSession.CompareArg(Imap4ResponseFactory.StoreBuf, buf, nextToken, num - nextToken))
				{
					array = Imap4ResponseFactory.StoreBuf;
					protocolRequest = new Imap4RequestStore(this, @string, text);
				}
				else if (BaseSession.CompareArg(Imap4ResponseFactory.SelectBuf, buf, nextToken, num - nextToken))
				{
					array = Imap4ResponseFactory.SelectBuf;
					protocolRequest = new Imap4RequestSelect(this, @string, text);
				}
				else if (BaseSession.CompareArg(Imap4ResponseFactory.StatusBuf, buf, nextToken, num - nextToken))
				{
					array = Imap4ResponseFactory.StatusBuf;
					protocolRequest = new Imap4RequestStatus(this, @string, text);
				}
				else if (BaseSession.CompareArg(Imap4ResponseFactory.StartTlsBuf, buf, nextToken, num - nextToken))
				{
					array = Imap4ResponseFactory.StartTlsBuf;
					protocolRequest = new Imap4RequestStartTls(this, @string, text);
				}
				else if (BaseSession.CompareArg(Imap4ResponseFactory.SubscribeBuf, buf, nextToken, num - nextToken))
				{
					array = Imap4ResponseFactory.SubscribeBuf;
					protocolRequest = new Imap4RequestSubscribe(this, @string, text);
				}
				else if (BaseSession.CompareArg(Imap4ResponseFactory.SearchBuf, buf, nextToken, num - nextToken))
				{
					array = Imap4ResponseFactory.SearchBuf;
					protocolRequest = new Imap4RequestSearch(this, @string, text);
				}
				break;
			case 'u':
				if (BaseSession.CompareArg(Imap4ResponseFactory.UidBuf, buf, nextToken, num - nextToken))
				{
					protocolRequest = new Imap4RequestUid(this, @string, text);
					array = Encoding.ASCII.GetBytes(base.CommandName);
				}
				else if (BaseSession.CompareArg(Imap4ResponseFactory.UnsubscribeBuf, buf, nextToken, num - nextToken))
				{
					array = Imap4ResponseFactory.UnsubscribeBuf;
					protocolRequest = new Imap4RequestUnsubscribe(this, @string, text);
				}
				break;
			case 'x':
				if (BaseSession.CompareArg(Imap4ResponseFactory.XproxyBuf, buf, nextToken, num - nextToken))
				{
					array = Imap4ResponseFactory.XproxyBuf;
					protocolRequest = new Imap4RequestXproxy(this, @string, text);
				}
				break;
			}
			if (protocolRequest == null)
			{
				protocolRequest = new Imap4RequestInvalid(this, @string, "Command Error. 12");
			}
			if (protocolRequest is Imap4RequestInvalid)
			{
				base.CommandName = "InvalidCommand";
				base.Parameters = Encoding.ASCII.GetString(buf, offset, size);
			}
			else
			{
				if (string.IsNullOrEmpty(base.CommandName))
				{
					base.CommandName = Encoding.ASCII.GetString(array);
				}
				base.Parameters = text;
			}
			if (base.Session.LightLogSession != null)
			{
				if (protocolRequest is Imap4RequestInvalid)
				{
					base.Session.LightLogSession.BeginCommand(Imap4ResponseFactory.InvalidBuf);
					base.Session.LightLogSession.Parameters = Encoding.ASCII.GetString(buf, offset, size);
				}
				else
				{
					base.Session.LightLogSession.BeginCommand(array);
					base.Session.LightLogSession.Parameters = text;
				}
			}
			return protocolRequest;
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00005B9C File Offset: 0x00003D9C
		public override ProtocolResponse ProcessIncompleteRequest(ProtocolRequest request)
		{
			base.IncompleteRequest = request;
			Imap4Request imap4Request = (Imap4Request)base.IncompleteRequest;
			if (imap4Request.SendSyncResponse)
			{
				ProtocolResponse result = imap4Request.SyncResponse(request);
				imap4Request.SendSyncResponse = false;
				return result;
			}
			return null;
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00005BD6 File Offset: 0x00003DD6
		public override void PreProcessRequest(ProtocolRequest request)
		{
			base.PreProcessRequest(request);
			if (!this.NotificationManager.InRealTimeMode)
			{
				this.NotificationManager.ProcessNotifications();
			}
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00005BF8 File Offset: 0x00003DF8
		public ProtocolResponse ProcessRawData(byte[] data, int offset, int dataLength, out int bytesConsumed)
		{
			if (base.IncompleteRequest == null)
			{
				bytesConsumed = 0;
				return null;
			}
			((Imap4Request)base.IncompleteRequest).AddData(data, offset, dataLength, out bytesConsumed);
			ProtocolResponse result;
			if (base.IncompleteRequest.ParseResult > ParseResult.success)
			{
				result = this.ProcessParseError(base.IncompleteRequest);
				base.IncompleteRequest = null;
				return result;
			}
			if (!base.IncompleteRequest.IsComplete)
			{
				result = this.ProcessIncompleteRequest(base.IncompleteRequest);
			}
			else
			{
				result = base.ConnectToTheStoreAndProcessTheRequest(base.IncompleteRequest);
				base.IncompleteRequest = null;
			}
			return result;
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00005C80 File Offset: 0x00003E80
		public override ProtocolResponse ProcessInvalidState(ProtocolRequest request)
		{
			string text = (this.SessionState == Imap4State.AuthenticatedButFailed) ? "User is authenticated but not connected." : "Command received in Invalid state.";
			Imap4Request request2 = (Imap4Request)request;
			Imap4Response imap4Response;
			if ((base.InvalidCommands += 1U) > 2U)
			{
				text += "\r\n* BYE Connection closed. 14";
				imap4Response = new Imap4Response(request2, Imap4Response.Type.bad, text);
				imap4Response.IsDisconnectResponse = true;
			}
			else
			{
				imap4Response = new Imap4Response(request2, Imap4Response.Type.bad, text);
			}
			return imap4Response;
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00005CE8 File Offset: 0x00003EE8
		public override ProtocolResponse ProcessParseError(ProtocolRequest request)
		{
			Imap4Request request2 = (Imap4Request)request;
			Imap4Response.Type type = Imap4Response.Type.bad;
			string text;
			switch (request.ParseResult)
			{
			case ParseResult.invalidArgument:
				text = "Command Argument Error. 11";
				break;
			case ParseResult.invalidNumberOfArguments:
				text = "Command Argument Error. 12";
				break;
			case ParseResult.invalidMessageSet:
				text = "[*] The specified message set is invalid.";
				break;
			case ParseResult.invalidCharset:
				text = "[BADCHARSET (US-ASCII)] The specified charset is not supported.";
				type = Imap4Response.Type.no;
				break;
			default:
				throw new InvalidOperationException("ProcessParseError is called when there is no error");
			}
			Imap4Response imap4Response;
			if (type == Imap4Response.Type.bad && (base.InvalidCommands += 1U) > 2U)
			{
				text += "\r\n* BYE Connection closed. 14";
				imap4Response = new Imap4Response(request2, type, text);
				imap4Response.IsDisconnectResponse = true;
			}
			else
			{
				imap4Response = new Imap4Response(request2, type, text);
			}
			return imap4Response;
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00005D94 File Offset: 0x00003F94
		public override ProtocolResponse CommandIsNotAllASCII(byte[] buf, int offset, int size)
		{
			int num;
			int nextToken = Imap4Session.GetNextToken(buf, offset, size, out num);
			if (nextToken != offset || num - nextToken > size)
			{
			}
			string @string = Encoding.ASCII.GetString(buf, nextToken, num - nextToken);
			string text = @string + " BAD Command Error. 11";
			ProtocolResponse protocolResponse;
			if ((base.InvalidCommands += 1U) > 2U)
			{
				text += "\r\n* BYE Connection closed. 14";
				protocolResponse = new ProtocolResponse(text);
				protocolResponse.IsDisconnectResponse = true;
			}
			else
			{
				protocolResponse = new ProtocolResponse(text);
			}
			return protocolResponse;
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00005E16 File Offset: 0x00004016
		public override ProtocolResponse ProcessException(ProtocolRequest request, Exception exception)
		{
			return this.ProcessException(request, exception, "Server Unavailable. 15");
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00005E28 File Offset: 0x00004028
		public override ProtocolResponse ProcessException(ProtocolRequest request, Exception exception, string responseString)
		{
			ProtocolBaseServices.SessionTracer.TraceError<Exception>(base.Session.SessionId, "Exception caught: {0}", exception);
			if (exception is StorageTransientException)
			{
				RatePerfCounters.IncrementExceptionPerfCounter(2);
			}
			else if (exception is StoragePermanentException)
			{
				RatePerfCounters.IncrementExceptionPerfCounter(3);
			}
			else if (exception is ADTransientException || exception is ServiceDiscoveryTransientException)
			{
				RatePerfCounters.IncrementExceptionPerfCounter(4);
			}
			else if (exception is ServiceDiscoveryPermanentException || exception is ADOperationException || exception is ADExternalException)
			{
				RatePerfCounters.IncrementExceptionPerfCounter(5);
			}
			else if (exception is ConnectionFailedTransientException)
			{
				RatePerfCounters.IncrementExceptionPerfCounter(0);
			}
			else if (exception is MailboxOfflineException)
			{
				RatePerfCounters.IncrementExceptionPerfCounter(1);
			}
			else if (exception is TransientException)
			{
				RatePerfCounters.IncrementExceptionPerfCounter(6);
			}
			if (base.Session.LightLogSession != null)
			{
				base.Session.LightLogSession.ExceptionCaught = exception;
			}
			Imap4Request request2 = (Imap4Request)request;
			ProtocolResponse protocolResponse;
			if ((base.InvalidCommands += 1U) > 2U)
			{
				responseString += "\r\n* BYE Connection closed. 14";
				protocolResponse = new Imap4Response(request2, Imap4Response.Type.no, responseString);
				protocolResponse.IsDisconnectResponse = true;
			}
			else if (exception is ConnectionFailedTransientException || exception is MailboxInSiteFailoverException || exception is MailboxCrossSiteFailoverException || exception is ConnectionFailedPermanentException)
			{
				protocolResponse = new Imap4Response(request2, Imap4Response.Type.no, responseString + "\r\n* BYE Connection closed.");
				protocolResponse.IsDisconnectResponse = true;
			}
			else
			{
				protocolResponse = new Imap4Response(request2, Imap4Response.Type.no, responseString);
			}
			base.Session.SetDiagnosticValue(ConditionalHandlerSchema.Exception, exception.ToString());
			base.Session.SetDiagnosticValue(PopImapConditionalHandlerSchema.ResponseType, (protocolResponse as Imap4Response).ResponseType);
			base.AddExceptionToCache(exception);
			return protocolResponse;
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00005FB4 File Offset: 0x000041B4
		public override ProtocolResponse AuthenticationDone(ProtocolRequest authenticateRequest, ResponseFactory.AuthenticationResult authenticationResult)
		{
			Imap4RequestAuthenticate imap4RequestAuthenticate = (Imap4RequestAuthenticate)authenticateRequest;
			if (imap4RequestAuthenticate == null)
			{
				return null;
			}
			switch (authenticationResult)
			{
			case ResponseFactory.AuthenticationResult.success:
				this.sessionState = Imap4State.Authenticated;
				goto IL_4A;
			case ResponseFactory.AuthenticationResult.authenticatedButFailed:
				this.sessionState = Imap4State.AuthenticatedButFailed;
				goto IL_4A;
			case ResponseFactory.AuthenticationResult.authenticatedAsCafe:
				this.sessionState = Imap4State.AuthenticatedAsCafe;
				goto IL_4A;
			}
			this.sessionState = Imap4State.Nonauthenticated;
			IL_4A:
			return imap4RequestAuthenticate.AuthenticationDone(base.LoginAttempts += 1U, authenticationResult);
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00006023 File Offset: 0x00004223
		public bool NothingToDo()
		{
			return this.SelectedMailbox == null || this.SelectedMailbox.MailboxDoesNotExist;
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x0000603F File Offset: 0x0000423F
		public override void DoPostLoginTasks()
		{
			this.RegisterFolderHierarchHandler();
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00006060 File Offset: 0x00004260
		protected override IEnumerable<EmailTransportService> GetProxyDestinations(ExchangePrincipal exchangePrincipal)
		{
			ServiceTopology serviceTopology = ProtocolBaseServices.IsMultiTenancyEnabled ? ServiceTopology.GetCurrentLegacyServiceTopology("f:\\15.00.1497\\sources\\dev\\PopImap\\src\\Imap4\\Imap4ResponseFactory.cs", "GetProxyDestinations", 1279) : ServiceTopology.GetCurrentServiceTopology("f:\\15.00.1497\\sources\\dev\\PopImap\\src\\Imap4\\Imap4ResponseFactory.cs", "GetProxyDestinations", 1279);
			return serviceTopology.FindAll<Imap4Service>(exchangePrincipal, ClientAccessType.Internal, (Imap4Service service) => ResponseFactory.CanProxyTo(service, exchangePrincipal), "f:\\15.00.1497\\sources\\dev\\PopImap\\src\\Imap4\\Imap4ResponseFactory.cs", "GetProxyDestinations", 1280).Cast<EmailTransportService>();
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x000060D9 File Offset: 0x000042D9
		protected override int GetE15MbxProxyPort(string e15MbxFqdn)
		{
			return base.GetE15MbxProxyPort<Imap4AdConfiguration>(e15MbxFqdn, Imap4ResponseFactory.proxyPortForE15Mbx, false, "");
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x000060ED File Offset: 0x000042ED
		protected override int GetE15MbxProxyPort(string e15MbxFqdn, bool isCrossForest, string userDomain)
		{
			return base.GetE15MbxProxyPort<Imap4AdConfiguration>(e15MbxFqdn, Imap4ResponseFactory.proxyPortForE15Mbx, isCrossForest, userDomain);
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00006100 File Offset: 0x00004300
		public void ReloadAllNotificationHandlers()
		{
			ProtocolBaseServices.SessionTracer.TraceDebug(base.Session.SessionId, "Start ReloadAllNotificationHandlers.");
			lock (base.Store)
			{
				bool flag2 = false;
				try
				{
					if (!base.IsStoreConnected)
					{
						base.ConnectToTheStore();
						flag2 = true;
					}
					try
					{
						this.notificationManager.UnregisterAllHandlers();
					}
					catch (LocalizedException)
					{
					}
					this.RegisterFolderHierarchHandler();
					if (this.SelectedMailbox != null)
					{
						ProtocolBaseServices.SessionTracer.TraceDebug(base.Session.SessionId, "Reload selected mailbox.");
						this.selectedMailbox.ExploreMailbox(this.SelectedMailbox.ExamineMode, true, true);
						this.notificationManager.RegisterHandler(this.selectedMailbox.DataAccessView);
						this.notificationManager.RegisterHandler(this.selectedMailbox.FastQueryView);
					}
				}
				finally
				{
					if (flag2)
					{
						base.DisconnectFromTheStore();
					}
				}
			}
			Imap4CountersInstance imap4CountersInstance = ((Imap4VirtualServer)base.Session.VirtualServer).Imap4CountersInstance;
			imap4CountersInstance.PerfCounter_ViewReload_Total.Increment();
			ProtocolBaseServices.SessionTracer.TraceDebug(base.Session.SessionId, "End ReloadAllNotificationHandlers.");
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00006244 File Offset: 0x00004444
		public override bool DoProxyConnect(byte[] buf, int offset, int size, ProxySession proxySession)
		{
			IProxyLogin proxyLogin = (IProxyLogin)base.IncompleteRequest;
			if (base.Session.Disposed)
			{
				ProtocolBaseServices.SessionTracer.TraceDebug(base.Session.SessionId, "Incoming session is disposed, nothing to do.");
				proxySession.State = ProxySession.ProxyState.failed;
				return false;
			}
			if (proxySession.Disposed)
			{
				ProtocolBaseServices.SessionTracer.TraceDebug(base.Session.SessionId, "Outgoing proxy session is disposed, nothing to do.");
				proxySession.State = ProxySession.ProxyState.failed;
				return false;
			}
			this.ProcessProxyResponse(buf, ref offset, ref size, proxySession);
			ProtocolBaseServices.SessionTracer.TraceDebug<ProxySession.ProxyState>(base.Session.SessionId, "Entering Imap4ResponseFactory proxy state machine with ProxySession.State {0}", proxySession.State);
			switch (proxySession.State)
			{
			case ProxySession.ProxyState.failed:
				return false;
			case ProxySession.ProxyState.initTls:
			{
				string s = "1 STARTTLS\r\n";
				base.Session.LogInformation("Sending STARTTLS to the BE server.", new object[0]);
				if (!proxySession.SendToClient(new StringResponseItem(s)))
				{
					proxySession.State = ProxySession.ProxyState.failed;
					return false;
				}
				proxySession.State = ProxySession.ProxyState.startTls;
				return false;
			}
			case ProxySession.ProxyState.startTls:
				base.Session.LogInformation("Negotiating TLS against BE server.", new object[0]);
				proxySession.Connection.BeginNegotiateTlsAsClient(new AsyncCallback(base.ProxyConnectionEncryptionComplete), proxySession);
				proxySession.State = ProxySession.ProxyState.initialization;
				return false;
			case ProxySession.ProxyState.initialization:
				if (base.Session.ProxyToLegacyServer)
				{
					if (proxyLogin == null)
					{
						proxySession.State = ProxySession.ProxyState.failed;
						return false;
					}
					string text = proxyLogin.UserName;
					if (!string.IsNullOrEmpty(base.Mailbox))
					{
						text = text + "/" + base.Mailbox;
					}
					base.Session.LogInformation("Sending LOGIN {0} <password> to the BE server.", new object[]
					{
						text.Replace("\\", "\\\\").Replace("\"", "\\\"")
					});
					string text2 = string.Format("1 LOGIN \"{0}\" \"", text);
					Imap4BufferBuilder imap4BufferBuilder = new Imap4BufferBuilder(text2.Length + 32);
					imap4BufferBuilder.Append(text2);
					try
					{
						imap4BufferBuilder.Append(proxyLogin.Password);
					}
					catch (ArgumentException ex)
					{
						imap4BufferBuilder.Reset();
						ProtocolBaseServices.SessionTracer.TraceDebug(base.Session.SessionId, ex.ToString());
						proxySession.State = ProxySession.ProxyState.failed;
						return false;
					}
					imap4BufferBuilder.Append(34);
					imap4BufferBuilder.Append(Strings.CRLFByteArray);
					imap4BufferBuilder.RemoveUnusedBufferSpace();
					if (!proxySession.SendToClient(new SecureBufferResponseItem(imap4BufferBuilder.GetBuffer())))
					{
						imap4BufferBuilder.Reset();
						proxySession.State = ProxySession.ProxyState.failed;
						return false;
					}
					proxySession.State = ProxySession.ProxyState.getNamespace;
					return false;
				}
				else
				{
					base.Session.LogInformation("Sending CAPABILITY to the BE server.", new object[0]);
					if (!proxySession.SendToClient(new StringResponseItem("1 CAPABILITY\r\n")))
					{
						proxySession.State = ProxySession.ProxyState.failed;
						return false;
					}
					proxySession.State = ProxySession.ProxyState.waitCapaOk;
					return false;
				}
				break;
			case ProxySession.ProxyState.waitCapaOk:
				if (base.ActualCafeAuthDone)
				{
					proxySession.State = (base.ServerToServerAuthEnabled ? ProxySession.ProxyState.sendAuthS2S : ProxySession.ProxyState.failed);
					return false;
				}
				proxySession.State = ProxySession.ProxyState.sendAuthPlain;
				return false;
			case ProxySession.ProxyState.sendAuthPlain:
				base.Session.LogInformation("Sending AUTHENTICATE PLAIN to the BE server.", new object[0]);
				if (!proxySession.SendToClient(new BufferResponseItem(Imap4ResponseFactory.AuthPlainCommand)))
				{
					proxySession.State = ProxySession.ProxyState.failed;
					return false;
				}
				proxySession.State = ProxySession.ProxyState.sendAuthBlob;
				return false;
			case ProxySession.ProxyState.sendAuthS2S:
			{
				base.Session.LogInformation("Sending AUTHENTICATE KERBEROS to the BE server for S2S auth.", new object[0]);
				if (!proxySession.SendToClient(new BufferResponseItem(Imap4ResponseFactory.AuthS2SCommand)))
				{
					proxySession.State = ProxySession.ProxyState.failed;
					return false;
				}
				proxySession.State = ProxySession.ProxyState.sendS2SAuthBlob;
				this.clientContext = new AuthenticationContext();
				SecurityStatus securityStatus = this.clientContext.InitializeForOutboundExchangeAuth("SHA256", "IMAP/" + proxyLogin.ProxyDestination.Substring(0, proxyLogin.ProxyDestination.IndexOf('.')), base.Session.ProxySession.Connection.RemoteCertificate.GetPublicKey(), base.Session.ProxySession.Connection.TlsEapKey);
				if (securityStatus != SecurityStatus.OK)
				{
					base.Session.LogInformation("S2S Auth InitializeForOutboundNegotiate failed with status {0}", new object[]
					{
						securityStatus
					});
					proxySession.State = ProxySession.ProxyState.failed;
					return false;
				}
				return false;
			}
			case ProxySession.ProxyState.sendAuthBlob:
			{
				byte[] plainAuthBlob = base.GetPlainAuthBlob();
				if (plainAuthBlob == null)
				{
					base.Session.LogInformation("No AUTH blob to send.", new object[0]);
					proxySession.State = ProxySession.ProxyState.failed;
					return false;
				}
				base.Session.LogInformation("Sending AUTH blob to the BE server.", new object[0]);
				if (!proxySession.SendToClient(new SecureBufferResponseItem(plainAuthBlob)))
				{
					proxySession.State = ProxySession.ProxyState.failed;
					return false;
				}
				if (ProtocolBaseServices.GCCEnabledWithKeys)
				{
					proxySession.State = ProxySession.ProxyState.sendXproxy;
					return false;
				}
				proxySession.State = ProxySession.ProxyState.authenticated;
				return false;
			}
			case ProxySession.ProxyState.sendS2SAuthBlob:
			{
				byte[] array;
				SecurityStatus securityStatus = this.clientContext.NegotiateSecurityContext(this.serverAuthResponse, offset, size, out array);
				ProtocolBaseServices.SessionTracer.TraceDebug<SecurityStatus>(base.Session.SessionId, "sendS2SAuthBlob NegotiateSecurityContext status {0}", securityStatus);
				SecurityStatus securityStatus2 = securityStatus;
				if (securityStatus2 != SecurityStatus.OK)
				{
					if (securityStatus2 != SecurityStatus.ContinueNeeded)
					{
						base.SetSessionError(string.Format("sendS2SAuthBlob NegotiateSecurityContext status {0}", securityStatus));
						proxySession.State = ProxySession.ProxyState.failed;
						return false;
					}
					if (array == null)
					{
						base.Session.LogInformation("sendS2SAuthBlob No AUTH blob to send.", new object[0]);
						proxySession.State = ProxySession.ProxyState.failed;
						return false;
					}
				}
				else if (array == null)
				{
					proxySession.State = ProxySession.ProxyState.sendXproxy3;
				}
				if (array == null)
				{
					return false;
				}
				base.Session.LogInformation("Sending KERBEROS AUTH blob to the BE server with cafe hostname.", new object[0]);
				byte[] bytes = Encoding.ASCII.GetBytes('\0' + Dns.GetHostName());
				Array.Resize<byte>(ref array, array.Length + bytes.Length);
				Array.Copy(bytes, 0, array, array.Length - bytes.Length, bytes.Length);
				if (!proxySession.SendToClient(new SecureBufferResponseItem(array)))
				{
					proxySession.State = ProxySession.ProxyState.failed;
					return false;
				}
				return false;
			}
			case ProxySession.ProxyState.authenticated:
				proxySession.State = ProxySession.ProxyState.completed;
				return true;
			case ProxySession.ProxyState.sendXproxy:
			{
				string text3 = string.Format("1 XPROXY {0} {1} {2}\r\n", GccUtils.GetAuthStringForThisServer(), base.Session.RemoteEndPoint.Address, base.Session.LocalEndPoint.Address);
				base.Session.LogInformation("Sending {0} to the BE server.", new object[]
				{
					text3.Trim()
				});
				if (!proxySession.SendToClient(new StringResponseItem(text3)))
				{
					proxySession.State = ProxySession.ProxyState.failed;
					return false;
				}
				proxySession.State = ProxySession.ProxyState.authenticated;
				return false;
			}
			case ProxySession.ProxyState.sendXproxy3:
			{
				string serverAuthXProxyCommand = base.GetServerAuthXProxyCommand("1 XPROXY");
				base.Session.LogInformation("Sending {0} to the BE server.", new object[]
				{
					serverAuthXProxyCommand.Trim()
				});
				if (!proxySession.SendToClient(new StringResponseItem(serverAuthXProxyCommand)))
				{
					proxySession.State = ProxySession.ProxyState.failed;
					return false;
				}
				proxySession.State = ProxySession.ProxyState.authenticated;
				return false;
			}
			case ProxySession.ProxyState.getNamespace:
			{
				ProtocolBaseServices.SessionTracer.TraceDebug(base.Session.SessionId, "Connection to BE server authenticated.");
				string s2 = "1 NAMESPACE\r\n";
				base.Session.LogInformation("Sending NAMESPACE to the BE server.", new object[0]);
				if (!proxySession.SendToClient(new StringResponseItem(s2)))
				{
					proxySession.State = ProxySession.ProxyState.failed;
					return false;
				}
				proxySession.State = ProxySession.ProxyState.waitingOK;
				return false;
			}
			case ProxySession.ProxyState.waitingOK:
				proxySession.State = ProxySession.ProxyState.authenticated;
				return false;
			}
			throw new InvalidOperationException("This should never happen!");
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00006984 File Offset: 0x00004B84
		internal ProtocolResponse DoConnect(Imap4RequestLogin loginRequest, SecureString password)
		{
			ProtocolResponse protocolResponse = null;
			base.IncompleteRequest = loginRequest;
			try
			{
				bool flag;
				if (base.TryToConnect(password, out flag))
				{
					protocolResponse = new Imap4Response(loginRequest, Imap4Response.Type.ok, "LOGIN completed.");
					this.sessionState = Imap4State.Authenticated;
					base.DisconnectFromTheStore();
					base.Session.SetDiagnosticValue(PopImapConditionalHandlerSchema.ResponseType, "OK");
				}
				else
				{
					this.sessionState = (flag ? Imap4State.AuthenticatedButFailed : Imap4State.Nonauthenticated);
					if (ResponseFactory.CheckOnlyAuthenticationStatusEnabled && flag && !ProtocolBaseServices.AuthErrorReportEnabled(base.UserName) && base.ProtocolUser.IsEnabled)
					{
						ProtocolBaseServices.SessionTracer.TraceDebug<string>(base.Session.SessionId, "User {0} login succeeded but failed to create proxy connection.", base.UserName);
						protocolResponse = new Imap4Response(loginRequest, Imap4Response.Type.ok, "LOGIN completed.");
					}
					else
					{
						ProtocolBaseServices.SessionTracer.TraceDebug<string>(base.Session.SessionId, "Failed to login user {0}.", base.UserName);
						if ((ulong)(base.LoginAttempts += 1U) < (ulong)((long)base.Session.Server.MaxFailedLoginAttempts))
						{
							protocolResponse = new Imap4Response(loginRequest, Imap4Response.Type.no, "LOGIN failed.");
						}
						else
						{
							protocolResponse = new Imap4Response(loginRequest, Imap4Response.Type.no, "LOGIN failed.\r\n* BYE Connection is closed. 14");
							protocolResponse.IsDisconnectResponse = true;
						}
					}
					base.Session.SetDiagnosticValue(PopImapConditionalHandlerSchema.ResponseType, "Err");
					if (base.Session[ConditionalHandlerSchema.Exception] == null)
					{
						base.Session.SetDiagnosticValue(ConditionalHandlerSchema.Exception, "LoginFailed");
					}
				}
			}
			finally
			{
				base.IncompleteRequest = null;
			}
			return protocolResponse;
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00006B08 File Offset: 0x00004D08
		protected override void ReloadStoreStates()
		{
			this.ReloadAllNotificationHandlers();
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00006B10 File Offset: 0x00004D10
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (this.folderHierarchyHandler != null)
				{
					this.folderHierarchyHandler.Dispose();
					this.folderHierarchyHandler = null;
				}
				if (this.notificationManager != null)
				{
					this.notificationManager.Dispose();
					this.notificationManager = null;
				}
			}
			finally
			{
				this.selectedMailbox = null;
				base.Dispose(disposing);
			}
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00006B74 File Offset: 0x00004D74
		protected override ProxySession NewProxySession(NetworkConnection connection)
		{
			return new Imap4ProxySession(this, connection);
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00006B80 File Offset: 0x00004D80
		private void ProcessProxyResponse(byte[] responseBuf, ref int offset, ref int size, ProxySession proxySession)
		{
			ProtocolBaseServices.SessionTracer.TraceDebug<ProxySession.ProxyState>(base.Session.SessionId, "Processing response for ProxySession.State : {0}", proxySession.State);
			if (responseBuf == null)
			{
				ProtocolBaseServices.SessionTracer.TraceDebug(base.Session.SessionId, "Empty buffer");
				return;
			}
			base.TraceProxyResponse(responseBuf, offset, size);
			if (proxySession.State == ProxySession.ProxyState.sendAuthBlob)
			{
				if (!BaseSession.CompareArg(Imap4ResponseFactory.PlusBuf, responseBuf, offset, size))
				{
					proxySession.State = ProxySession.ProxyState.failed;
				}
				return;
			}
			if (proxySession.State == ProxySession.ProxyState.sendS2SAuthBlob && size > 0 && responseBuf[offset] == Imap4ResponseFactory.PlusBuf[0])
			{
				if (size > 2)
				{
					this.serverAuthResponse = responseBuf;
					offset += 2;
					size -= 2;
					return;
				}
				this.serverAuthResponse = null;
				offset = 0;
				size = 0;
				return;
			}
			else
			{
				Imap4ProxySession imap4ProxySession = (Imap4ProxySession)proxySession;
				string @string = Encoding.ASCII.GetString(responseBuf, offset, size);
				if (@string == null)
				{
					ProtocolBaseServices.SessionTracer.TraceDebug(base.Session.SessionId, "Null response");
					return;
				}
				bool flag = @string.Substring(@string.IndexOf(' ') + 1).StartsWith("OK", StringComparison.OrdinalIgnoreCase);
				ProtocolBaseServices.SessionTracer.TraceDebug<string, ProxySession.ProxyState>(base.Session.SessionId, "Processing response \"{0}\" for ProxySession.State {1}", @string, proxySession.State);
				ProxySession.ProxyState state = proxySession.State;
				if (state != ProxySession.ProxyState.waitCapaOk)
				{
					switch (state)
					{
					case ProxySession.ProxyState.sendS2SAuthBlob:
						proxySession.State = (flag ? ProxySession.ProxyState.sendXproxy3 : ProxySession.ProxyState.failed);
						return;
					case ProxySession.ProxyState.authenticated:
					{
						IProxyLogin proxyLogin = (IProxyLogin)base.IncompleteRequest;
						if (flag)
						{
							return;
						}
						if (!ProtocolBaseServices.GCCEnabledWithKeys)
						{
							proxySession.State = ProxySession.ProxyState.failed;
						}
						if (!string.IsNullOrEmpty(proxyLogin.AuthenticationError))
						{
							return;
						}
						Match match = ResponseFactory.AuthErrorParser.Match(@string);
						if (!match.Success)
						{
							return;
						}
						if (match.Groups["authError"].Success)
						{
							proxyLogin.AuthenticationError = match.Groups["authError"].Value;
						}
						else
						{
							proxyLogin.AuthenticationError = null;
						}
						if (match.Groups["proxy"].Success)
						{
							IProxyLogin proxyLogin2 = proxyLogin;
							proxyLogin2.ProxyDestination = proxyLogin2.ProxyDestination + "," + match.Groups["proxy"].Value;
							return;
						}
						return;
					}
					case ProxySession.ProxyState.getNamespace:
					{
						Match match2 = Imap4ResponseFactory.namespaceParser.Match(@string);
						if (!flag || !match2.Success)
						{
							proxySession.State = ProxySession.ProxyState.failed;
							return;
						}
						if (match2.Groups["PublicFolders"].Success)
						{
							string text = match2.Groups["PublicFolders"].Value;
							if (text != null && text.Length >= 2 && text[0] == '"' && text[text.Length - 1] == '"')
							{
								text = text.Substring(1, text.Length - 2);
							}
							imap4ProxySession.PublicFolders = text;
							imap4ProxySession.PublicFolderNamespace = match2.Groups["pfNamespace"].Value;
							return;
						}
						imap4ProxySession.PublicFolders = null;
						imap4ProxySession.PublicFolderNamespace = null;
						return;
					}
					}
					if (!flag)
					{
						proxySession.State = ProxySession.ProxyState.failed;
					}
					return;
				}
				this.clientAccessRulesSupportedByTargetServer = @string.Contains("CLIENTACCESSRULES");
				base.ServerToServerAuthEnabled = @string.Contains("XPROXY3");
				return;
			}
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00006EBE File Offset: 0x000050BE
		private void RegisterFolderHierarchHandler()
		{
			if (this.folderHierarchyHandler != null)
			{
				this.folderHierarchyHandler.Dispose();
				this.folderHierarchyHandler = null;
			}
			this.folderHierarchyHandler = new Imap4FolderHierarchyHandler(this);
			this.NotificationManager.RegisterHandler(this.folderHierarchyHandler);
		}

		// Token: 0x04000051 RID: 81
		internal const string UnexpectedError = "Unexpected error.";

		// Token: 0x04000052 RID: 82
		internal const string InvalidState = "Command received in Invalid state.";

		// Token: 0x04000053 RID: 83
		internal const string AuthenticatedButFailedState = "User is authenticated but not connected.";

		// Token: 0x04000054 RID: 84
		internal const string CommandIsNotAllASCIIString = " BAD Command Error. 11";

		// Token: 0x04000055 RID: 85
		internal const string ServerUnavailable = "Server Unavailable. 15";

		// Token: 0x04000056 RID: 86
		internal const string TimeoutError = "* BYE Connection is closed. 13";

		// Token: 0x04000057 RID: 87
		internal const string DuplicateFolders = "Duplicate folders {0} were detected in the mailbox. Therefore the user's connection was disconnected.\r\n* BYE Connection is closed. 15";

		// Token: 0x04000058 RID: 88
		internal const string InvalidCommandResponse = "Command Error. 12";

		// Token: 0x04000059 RID: 89
		internal const string InvalidCommandResponseLastTime = "\r\n* BYE Connection closed. 14";

		// Token: 0x0400005A RID: 90
		internal const string ResponseBye = "\r\n* BYE Connection closed.";

		// Token: 0x0400005B RID: 91
		internal const string AccountInvalidatedMessage = "* BYE Session invalidated - {0}";

		// Token: 0x0400005C RID: 92
		internal static readonly byte[] LoginBuf = Encoding.ASCII.GetBytes("login");

		// Token: 0x0400005D RID: 93
		private static readonly Regex namespaceParser = new Regex("\\A\\* NAMESPACE (\\(\\(\".+?\"\\)\\)|NIL) (\\(\\(\".+?\"\\)\\)|NIL) (?<pfNamespace>\\(\\((?<PublicFolders>.+?) \".+?\"\\)\\)|NIL)\\z", RegexOptions.IgnoreCase | RegexOptions.Compiled);

		// Token: 0x0400005E RID: 94
		private static readonly byte[] AppendBuf = Encoding.ASCII.GetBytes("append");

		// Token: 0x0400005F RID: 95
		private static readonly byte[] AuthenticateBuf = Encoding.ASCII.GetBytes("authenticate");

		// Token: 0x04000060 RID: 96
		private static readonly byte[] CapabilityBuf = Encoding.ASCII.GetBytes("capability");

		// Token: 0x04000061 RID: 97
		private static readonly byte[] CheckBuf = Encoding.ASCII.GetBytes("check");

		// Token: 0x04000062 RID: 98
		private static readonly byte[] CloseBuf = Encoding.ASCII.GetBytes("close");

		// Token: 0x04000063 RID: 99
		private static readonly byte[] CopyBuf = Encoding.ASCII.GetBytes("copy");

		// Token: 0x04000064 RID: 100
		private static readonly byte[] CreateBuf = Encoding.ASCII.GetBytes("create");

		// Token: 0x04000065 RID: 101
		private static readonly byte[] DeleteBuf = Encoding.ASCII.GetBytes("delete");

		// Token: 0x04000066 RID: 102
		private static readonly byte[] ExamineBuf = Encoding.ASCII.GetBytes("examine");

		// Token: 0x04000067 RID: 103
		private static readonly byte[] ExpungeBuf = Encoding.ASCII.GetBytes("expunge");

		// Token: 0x04000068 RID: 104
		private static readonly byte[] FetchBuf = Encoding.ASCII.GetBytes("fetch");

		// Token: 0x04000069 RID: 105
		private static readonly byte[] IdleBuf = Encoding.ASCII.GetBytes("idle");

		// Token: 0x0400006A RID: 106
		private static readonly byte[] ListBuf = Encoding.ASCII.GetBytes("list");

		// Token: 0x0400006B RID: 107
		private static readonly byte[] LogoutBuf = Encoding.ASCII.GetBytes("logout");

		// Token: 0x0400006C RID: 108
		private static readonly byte[] LsubBuf = Encoding.ASCII.GetBytes("lsub");

		// Token: 0x0400006D RID: 109
		private static readonly byte[] MoveBuf = Encoding.ASCII.GetBytes("move");

		// Token: 0x0400006E RID: 110
		private static readonly byte[] IDBuf = Encoding.ASCII.GetBytes("id");

		// Token: 0x0400006F RID: 111
		private static readonly byte[] NamespaceBuf = Encoding.ASCII.GetBytes("namespace");

		// Token: 0x04000070 RID: 112
		private static readonly byte[] NoopBuf = Encoding.ASCII.GetBytes("noop");

		// Token: 0x04000071 RID: 113
		private static readonly byte[] RenameBuf = Encoding.ASCII.GetBytes("rename");

		// Token: 0x04000072 RID: 114
		private static readonly byte[] SearchBuf = Encoding.ASCII.GetBytes("search");

		// Token: 0x04000073 RID: 115
		private static readonly byte[] SelectBuf = Encoding.ASCII.GetBytes("select");

		// Token: 0x04000074 RID: 116
		private static readonly byte[] StartTlsBuf = Encoding.ASCII.GetBytes("starttls");

		// Token: 0x04000075 RID: 117
		private static readonly byte[] StatusBuf = Encoding.ASCII.GetBytes("status");

		// Token: 0x04000076 RID: 118
		private static readonly byte[] StoreBuf = Encoding.ASCII.GetBytes("store");

		// Token: 0x04000077 RID: 119
		private static readonly byte[] SubscribeBuf = Encoding.ASCII.GetBytes("subscribe");

		// Token: 0x04000078 RID: 120
		private static readonly byte[] UidBuf = Encoding.ASCII.GetBytes("uid");

		// Token: 0x04000079 RID: 121
		private static readonly byte[] XproxyBuf = Encoding.ASCII.GetBytes("xproxy");

		// Token: 0x0400007A RID: 122
		private static readonly byte[] UnsubscribeBuf = Encoding.ASCII.GetBytes("unsubscribe");

		// Token: 0x0400007B RID: 123
		private static readonly byte[] DoneBuf = Encoding.ASCII.GetBytes("done");

		// Token: 0x0400007C RID: 124
		private static readonly byte[] InvalidBuf = Encoding.ASCII.GetBytes("InvalidCommand");

		// Token: 0x0400007D RID: 125
		private static readonly byte[] AuthPlainCommand = Encoding.ASCII.GetBytes("1 AUTHENTICATE PLAIN\r\n");

		// Token: 0x0400007E RID: 126
		private static readonly byte[] AuthS2SCommand = Encoding.ASCII.GetBytes("1 AUTHENTICATE KERBEROS\r\n");

		// Token: 0x0400007F RID: 127
		private static readonly byte[] PlusBuf = Encoding.ASCII.GetBytes("+");

		// Token: 0x04000080 RID: 128
		private static MruDictionaryCache<string, int> proxyPortForE15Mbx = new MruDictionaryCache<string, int>(50, 5000, 1440);

		// Token: 0x04000081 RID: 129
		private Imap4State sessionState = Imap4State.Nonauthenticated;

		// Token: 0x04000082 RID: 130
		private Imap4State savedSessionState = Imap4State.Nonauthenticated;

		// Token: 0x04000083 RID: 131
		private string savedIdleTag;

		// Token: 0x04000084 RID: 132
		private Imap4Mailbox selectedMailbox;

		// Token: 0x04000085 RID: 133
		private MapiNotificationManager notificationManager;

		// Token: 0x04000086 RID: 134
		private Imap4FolderHierarchyHandler folderHierarchyHandler;

		// Token: 0x04000087 RID: 135
		private readonly bool moveEnabled;

		// Token: 0x04000088 RID: 136
		private readonly bool moveEnabledCafe;

		// Token: 0x04000089 RID: 137
		private readonly bool idEnabled;

		// Token: 0x0400008A RID: 138
		private readonly bool idEnabledCafe;

		// Token: 0x0400008B RID: 139
		private AuthenticationContext clientContext;

		// Token: 0x0400008C RID: 140
		private byte[] serverAuthResponse;
	}
}
