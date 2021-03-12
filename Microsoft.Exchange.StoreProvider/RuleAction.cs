using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi.Unmanaged;

namespace Microsoft.Mapi
{
	// Token: 0x0200022C RID: 556
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class RuleAction
	{
		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060009BF RID: 2495 RVA: 0x00030522 File Offset: 0x0002E722
		// (set) Token: 0x060009C0 RID: 2496 RVA: 0x0003052A File Offset: 0x0002E72A
		public uint UserFlags
		{
			get
			{
				return this._UserFlags;
			}
			set
			{
				this._UserFlags = value;
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060009C1 RID: 2497 RVA: 0x00030533 File Offset: 0x0002E733
		// (set) Token: 0x060009C2 RID: 2498 RVA: 0x0003053B File Offset: 0x0002E73B
		public RuleAction.Type ActionType
		{
			get
			{
				return this._Type;
			}
			set
			{
				this._Type = value;
			}
		}

		// Token: 0x060009C3 RID: 2499
		internal abstract int GetBytesToMarshal();

		// Token: 0x060009C4 RID: 2500
		internal unsafe abstract void MarshalToNative(_Action* pact, ref byte* pExtra);

		// Token: 0x060009C5 RID: 2501 RVA: 0x00030544 File Offset: 0x0002E744
		private unsafe void BaseMarshal(_Action* pact)
		{
			pact->ActType = (uint)this._Type;
			pact->Zero1 = IntPtr.Zero;
			pact->Zero2 = IntPtr.Zero;
			pact->ulFlags = this._UserFlags;
		}

		// Token: 0x060009C6 RID: 2502 RVA: 0x00030574 File Offset: 0x0002E774
		private RuleAction(RuleAction.Type type)
		{
			this._Type = type;
			this._UserFlags = 0U;
		}

		// Token: 0x060009C7 RID: 2503 RVA: 0x0003058A File Offset: 0x0002E78A
		private unsafe RuleAction(_Action* pact)
		{
			this._Type = (RuleAction.Type)pact->ActType;
			this._UserFlags = pact->ulFlags;
		}

		// Token: 0x060009C8 RID: 2504 RVA: 0x000305AA File Offset: 0x0002E7AA
		internal static bool Equals(RuleAction a, RuleAction b)
		{
			return a == b || (a != null && b != null);
		}

		// Token: 0x060009C9 RID: 2505 RVA: 0x000305BC File Offset: 0x0002E7BC
		internal unsafe static RuleAction Unmarshal(_Action* pact)
		{
			if (null != pact)
			{
				switch (pact->ActType)
				{
				case 1U:
				{
					RuleAction.MoveCopy moveCopy = new RuleAction.ExternalMove(pact);
					if (moveCopy.FolderIsInThisStore)
					{
						moveCopy = new RuleAction.InMailboxMove(pact);
					}
					return moveCopy;
				}
				case 2U:
				{
					RuleAction.MoveCopy moveCopy2 = new RuleAction.ExternalCopy(pact);
					if (moveCopy2.FolderIsInThisStore)
					{
						moveCopy2 = new RuleAction.InMailboxCopy(pact);
					}
					return moveCopy2;
				}
				case 3U:
					if ((pact->ActFlavor & 4294967292U) == 0U)
					{
						return new RuleAction.Reply(pact);
					}
					return null;
				case 4U:
					return new RuleAction.OOFReply(pact);
				case 5U:
					return new RuleAction.Defer(pact);
				case 6U:
				{
					RuleAction.Bounce.BounceCode scBounceCode = (RuleAction.Bounce.BounceCode)pact->union.scBounceCode;
					if (scBounceCode == RuleAction.Bounce.BounceCode.TooLarge || scBounceCode == RuleAction.Bounce.BounceCode.FormsMismatch || scBounceCode == RuleAction.Bounce.BounceCode.AccessDenied)
					{
						return new RuleAction.Bounce(pact);
					}
					return null;
				}
				case 7U:
					if ((pact->ActFlavor & 4294967280U) == 0U)
					{
						return new RuleAction.Forward(pact);
					}
					return null;
				case 8U:
					return new RuleAction.Delegate(pact);
				case 9U:
					return new RuleAction.Tag(pact);
				case 10U:
					return new RuleAction.Delete(pact);
				case 11U:
					return new RuleAction.MarkAsRead(pact);
				}
			}
			return null;
		}

		// Token: 0x04000FC1 RID: 4033
		private uint _UserFlags;

		// Token: 0x04000FC2 RID: 4034
		protected RuleAction.Type _Type;

		// Token: 0x0200022D RID: 557
		internal enum Type : uint
		{
			// Token: 0x04000FC4 RID: 4036
			OP_INVALID,
			// Token: 0x04000FC5 RID: 4037
			OP_MOVE,
			// Token: 0x04000FC6 RID: 4038
			OP_COPY,
			// Token: 0x04000FC7 RID: 4039
			OP_REPLY,
			// Token: 0x04000FC8 RID: 4040
			OP_OOF_REPLY,
			// Token: 0x04000FC9 RID: 4041
			OP_DEFER_ACTION,
			// Token: 0x04000FCA RID: 4042
			OP_BOUNCE,
			// Token: 0x04000FCB RID: 4043
			OP_FORWARD,
			// Token: 0x04000FCC RID: 4044
			OP_DELEGATE,
			// Token: 0x04000FCD RID: 4045
			OP_TAG,
			// Token: 0x04000FCE RID: 4046
			OP_DELETE,
			// Token: 0x04000FCF RID: 4047
			OP_MARK_AS_READ
		}

		// Token: 0x0200022E RID: 558
		public class MoveCopy : RuleAction
		{
			// Token: 0x17000104 RID: 260
			// (get) Token: 0x060009CA RID: 2506 RVA: 0x000306B8 File Offset: 0x0002E8B8
			// (set) Token: 0x060009CB RID: 2507 RVA: 0x000306C0 File Offset: 0x0002E8C0
			public byte[] FolderEntryID
			{
				get
				{
					return this._FolderEntryID;
				}
				set
				{
					this._FolderEntryID = value;
				}
			}

			// Token: 0x17000105 RID: 261
			// (get) Token: 0x060009CC RID: 2508 RVA: 0x000306C9 File Offset: 0x0002E8C9
			// (set) Token: 0x060009CD RID: 2509 RVA: 0x000306D1 File Offset: 0x0002E8D1
			public byte[] StoreEntryID
			{
				get
				{
					return this._StoreEntryID;
				}
				set
				{
					this._StoreEntryID = value;
				}
			}

			// Token: 0x17000106 RID: 262
			// (get) Token: 0x060009CE RID: 2510 RVA: 0x000306DA File Offset: 0x0002E8DA
			// (set) Token: 0x060009CF RID: 2511 RVA: 0x000306F1 File Offset: 0x0002E8F1
			public bool FolderIsInThisStore
			{
				get
				{
					return this._StoreEntryID == null || this._StoreEntryID.Length == 0;
				}
				set
				{
					if (value)
					{
						this._StoreEntryID = null;
					}
				}
			}

			// Token: 0x060009D0 RID: 2512 RVA: 0x000306FD File Offset: 0x0002E8FD
			public MoveCopy(RuleAction.Type Type, byte[] FolderEntryID) : base(Type)
			{
				this._FolderEntryID = FolderEntryID;
				this.FolderIsInThisStore = true;
			}

			// Token: 0x060009D1 RID: 2513 RVA: 0x00030714 File Offset: 0x0002E914
			public MoveCopy(RuleAction.Type Type, byte[] StoreEntryID, byte[] FolderEntryID) : base(Type)
			{
				this._StoreEntryID = StoreEntryID;
				this._FolderEntryID = FolderEntryID;
			}

			// Token: 0x060009D2 RID: 2514 RVA: 0x0003072C File Offset: 0x0002E92C
			internal unsafe MoveCopy(_Action* pact) : base(pact)
			{
				this._FolderEntryID = new byte[pact->union.actMoveCopy.cbFolderEntryID];
				Marshal.Copy((IntPtr)((void*)pact->union.actMoveCopy.lpbFolderEntryID), this._FolderEntryID, 0, this._FolderEntryID.Length);
				byte[] array = new byte[pact->union.actMoveCopy.cbStoreEntryID];
				Marshal.Copy((IntPtr)((void*)pact->union.actMoveCopy.lpbStoreEntryID), array, 0, array.Length);
				if (pact->union.actMoveCopy.cbStoreEntryID == 16 && new Guid(array).Equals(RuleAction.MoveCopy.InThisStoreGuid))
				{
					this.FolderIsInThisStore = true;
					return;
				}
				this._StoreEntryID = array;
			}

			// Token: 0x060009D3 RID: 2515 RVA: 0x000307F4 File Offset: 0x0002E9F4
			internal override int GetBytesToMarshal()
			{
				int num = 0;
				num += (_Action.SizeOf + 7 & -8);
				num += (this._FolderEntryID.Length + 7 & -8);
				return num + ((this.FolderIsInThisStore ? RuleAction.MoveCopy.InThisStoreBytes.Length : this._StoreEntryID.Length) + 7 & -8);
			}

			// Token: 0x060009D4 RID: 2516 RVA: 0x00030844 File Offset: 0x0002EA44
			internal unsafe override void MarshalToNative(_Action* pact, ref byte* pExtra)
			{
				base.BaseMarshal(pact);
				byte* ptr = pExtra;
				pExtra += (IntPtr)(this._FolderEntryID.Length + 7 & -8);
				pact->union.actMoveCopy.cbFolderEntryID = this._FolderEntryID.Length;
				pact->union.actMoveCopy.lpbFolderEntryID = ptr;
				Marshal.Copy(this._FolderEntryID, 0, (IntPtr)((void*)ptr), this._FolderEntryID.Length);
				byte[] array;
				if (this.FolderIsInThisStore)
				{
					array = RuleAction.MoveCopy.InThisStoreBytes;
				}
				else
				{
					array = this._StoreEntryID;
				}
				ptr = pExtra;
				pExtra += (IntPtr)(array.Length + 7 & -8);
				pact->union.actMoveCopy.cbStoreEntryID = array.Length;
				pact->union.actMoveCopy.lpbStoreEntryID = ptr;
				Marshal.Copy(array, 0, (IntPtr)((void*)ptr), array.Length);
			}

			// Token: 0x04000FD0 RID: 4048
			public static readonly Guid InThisStoreGuid = new Guid("62ef076a-3149-4920-b80c-4c3ee1cf2698");

			// Token: 0x04000FD1 RID: 4049
			public static readonly byte[] InThisStoreBytes = RuleAction.MoveCopy.InThisStoreGuid.ToByteArray();

			// Token: 0x04000FD2 RID: 4050
			private byte[] _StoreEntryID;

			// Token: 0x04000FD3 RID: 4051
			private byte[] _FolderEntryID;
		}

		// Token: 0x0200022F RID: 559
		public abstract class BaseReply : RuleAction
		{
			// Token: 0x060009D6 RID: 2518 RVA: 0x0003092B File Offset: 0x0002EB2B
			internal BaseReply(RuleAction.Type Type, byte[] ReplyTemplateMessageEntryID, Guid ReplyTemplateGuid, RuleAction.Reply.ActionFlags raf) : base(Type)
			{
				this._ReplyTemplateMessageEntryID = ReplyTemplateMessageEntryID;
				this._ReplyTemplateGuid = ReplyTemplateGuid;
				this._raf = raf;
			}

			// Token: 0x060009D7 RID: 2519 RVA: 0x0003094C File Offset: 0x0002EB4C
			internal unsafe BaseReply(_Action* pact) : base(pact)
			{
				this._raf = (RuleAction.Reply.ActionFlags)pact->ActFlavor;
				this._ReplyTemplateGuid = pact->union.actReply.guidReplyTemplate;
				if (pact->union.actReply.cbMessageEntryID != 0)
				{
					this._ReplyTemplateMessageEntryID = new byte[pact->union.actReply.cbMessageEntryID];
					Marshal.Copy((IntPtr)((void*)pact->union.actReply.lpbMessageEntryID), this._ReplyTemplateMessageEntryID, 0, this._ReplyTemplateMessageEntryID.Length);
					return;
				}
				this._ReplyTemplateMessageEntryID = null;
			}

			// Token: 0x060009D8 RID: 2520 RVA: 0x000309E0 File Offset: 0x0002EBE0
			internal override int GetBytesToMarshal()
			{
				int num = 0;
				num += (_Action.SizeOf + 7 & -8);
				if (this._ReplyTemplateMessageEntryID != null && this._ReplyTemplateMessageEntryID.Length != 0)
				{
					num += (this._ReplyTemplateMessageEntryID.Length + 7 & -8);
				}
				return num;
			}

			// Token: 0x060009D9 RID: 2521 RVA: 0x00030A20 File Offset: 0x0002EC20
			internal unsafe override void MarshalToNative(_Action* pact, ref byte* pExtra)
			{
				base.BaseMarshal(pact);
				pact->union.actReply.guidReplyTemplate = this._ReplyTemplateGuid;
				pact->ActFlavor = (uint)this._raf;
				if (this._ReplyTemplateMessageEntryID != null && this._ReplyTemplateMessageEntryID.Length != 0)
				{
					byte* ptr = pExtra;
					pExtra += (IntPtr)(this._ReplyTemplateMessageEntryID.Length + 7 & -8);
					pact->union.actReply.cbMessageEntryID = this._ReplyTemplateMessageEntryID.Length;
					pact->union.actReply.lpbMessageEntryID = ptr;
					Marshal.Copy(this._ReplyTemplateMessageEntryID, 0, (IntPtr)((void*)ptr), this._ReplyTemplateMessageEntryID.Length);
					return;
				}
				pact->union.actReply.cbMessageEntryID = 0;
				pact->union.actReply.lpbMessageEntryID = null;
			}

			// Token: 0x04000FD4 RID: 4052
			protected internal byte[] _ReplyTemplateMessageEntryID;

			// Token: 0x04000FD5 RID: 4053
			protected internal Guid _ReplyTemplateGuid;

			// Token: 0x04000FD6 RID: 4054
			protected internal RuleAction.Reply.ActionFlags _raf;
		}

		// Token: 0x02000230 RID: 560
		public abstract class FwdDelegate : RuleAction
		{
			// Token: 0x17000107 RID: 263
			// (get) Token: 0x060009DA RID: 2522 RVA: 0x00030AE4 File Offset: 0x0002ECE4
			// (set) Token: 0x060009DB RID: 2523 RVA: 0x00030AEC File Offset: 0x0002ECEC
			public AdrEntry[] Recipients
			{
				get
				{
					return this._Recipients;
				}
				set
				{
					this._Recipients = value;
				}
			}

			// Token: 0x060009DC RID: 2524 RVA: 0x00030AF5 File Offset: 0x0002ECF5
			internal FwdDelegate(RuleAction.Type Type, AdrEntry[] Recipients, RuleAction.Forward.ActionFlags faf) : base(Type)
			{
				this._Recipients = Recipients;
				this._faf = faf;
			}

			// Token: 0x060009DD RID: 2525 RVA: 0x00030B0C File Offset: 0x0002ED0C
			internal unsafe FwdDelegate(_Action* pact) : base(pact)
			{
				this._faf = (RuleAction.Forward.ActionFlags)pact->ActFlavor;
				this._Recipients = AdrList.Unmarshal(pact->union.lpadrlist);
			}

			// Token: 0x060009DE RID: 2526 RVA: 0x00030B38 File Offset: 0x0002ED38
			internal override int GetBytesToMarshal()
			{
				int num = 0;
				num += (_Action.SizeOf + 7 & -8);
				return num + AdrList.GetBytesToMarshal(this._Recipients);
			}

			// Token: 0x060009DF RID: 2527 RVA: 0x00030B63 File Offset: 0x0002ED63
			internal unsafe override void MarshalToNative(_Action* pact, ref byte* pExtra)
			{
				base.BaseMarshal(pact);
				pact->ActFlavor = (uint)this._faf;
				pact->union.lpadrlist = pExtra;
				AdrList.MarshalToNative(pExtra, this._Recipients);
				pExtra += (IntPtr)AdrList.GetBytesToMarshal(this._Recipients);
			}

			// Token: 0x04000FD7 RID: 4055
			protected RuleAction.Forward.ActionFlags _faf;

			// Token: 0x04000FD8 RID: 4056
			private AdrEntry[] _Recipients;
		}

		// Token: 0x02000231 RID: 561
		public class Bounce : RuleAction
		{
			// Token: 0x17000108 RID: 264
			// (get) Token: 0x060009E0 RID: 2528 RVA: 0x00030BA2 File Offset: 0x0002EDA2
			// (set) Token: 0x060009E1 RID: 2529 RVA: 0x00030BAA File Offset: 0x0002EDAA
			public RuleAction.Bounce.BounceCode Code
			{
				get
				{
					return this._sc;
				}
				set
				{
					this._sc = value;
				}
			}

			// Token: 0x060009E2 RID: 2530 RVA: 0x00030BB3 File Offset: 0x0002EDB3
			public Bounce(RuleAction.Bounce.BounceCode Code) : base(RuleAction.Type.OP_BOUNCE)
			{
				this._sc = Code;
			}

			// Token: 0x060009E3 RID: 2531 RVA: 0x00030BC3 File Offset: 0x0002EDC3
			internal unsafe Bounce(_Action* pact) : base(pact)
			{
				this._sc = (RuleAction.Bounce.BounceCode)pact->union.scBounceCode;
			}

			// Token: 0x060009E4 RID: 2532 RVA: 0x00030BE0 File Offset: 0x0002EDE0
			internal override int GetBytesToMarshal()
			{
				int num = 0;
				return num + (_Action.SizeOf + 7 & -8);
			}

			// Token: 0x060009E5 RID: 2533 RVA: 0x00030BFD File Offset: 0x0002EDFD
			internal unsafe override void MarshalToNative(_Action* pact, ref byte* pExtra)
			{
				base.BaseMarshal(pact);
				pact->union.scBounceCode = (uint)this._sc;
			}

			// Token: 0x04000FD9 RID: 4057
			private RuleAction.Bounce.BounceCode _sc;

			// Token: 0x02000232 RID: 562
			internal enum BounceCode : uint
			{
				// Token: 0x04000FDB RID: 4059
				TooLarge = 13U,
				// Token: 0x04000FDC RID: 4060
				FormsMismatch = 31U,
				// Token: 0x04000FDD RID: 4061
				AccessDenied = 38U
			}
		}

		// Token: 0x02000233 RID: 563
		public class InMailboxMove : RuleAction.MoveCopy
		{
			// Token: 0x060009E6 RID: 2534 RVA: 0x00030C17 File Offset: 0x0002EE17
			public InMailboxMove(byte[] FolderEntryID) : base(RuleAction.Type.OP_MOVE, FolderEntryID)
			{
			}

			// Token: 0x060009E7 RID: 2535 RVA: 0x00030C21 File Offset: 0x0002EE21
			internal unsafe InMailboxMove(_Action* pact) : base(pact)
			{
			}
		}

		// Token: 0x02000234 RID: 564
		public class ExternalMove : RuleAction.MoveCopy
		{
			// Token: 0x060009E8 RID: 2536 RVA: 0x00030C2A File Offset: 0x0002EE2A
			public ExternalMove(byte[] StoreEntryID, byte[] FolderEntryID) : base(RuleAction.Type.OP_MOVE, StoreEntryID, FolderEntryID)
			{
			}

			// Token: 0x060009E9 RID: 2537 RVA: 0x00030C35 File Offset: 0x0002EE35
			internal unsafe ExternalMove(_Action* pact) : base(pact)
			{
			}
		}

		// Token: 0x02000235 RID: 565
		public class InMailboxCopy : RuleAction.MoveCopy
		{
			// Token: 0x060009EA RID: 2538 RVA: 0x00030C3E File Offset: 0x0002EE3E
			public InMailboxCopy(byte[] FolderEntryID) : base(RuleAction.Type.OP_COPY, FolderEntryID)
			{
			}

			// Token: 0x060009EB RID: 2539 RVA: 0x00030C48 File Offset: 0x0002EE48
			internal unsafe InMailboxCopy(_Action* pact) : base(pact)
			{
			}
		}

		// Token: 0x02000236 RID: 566
		public class ExternalCopy : RuleAction.MoveCopy
		{
			// Token: 0x060009EC RID: 2540 RVA: 0x00030C51 File Offset: 0x0002EE51
			public ExternalCopy(byte[] StoreEntryID, byte[] FolderEntryID) : base(RuleAction.Type.OP_COPY, StoreEntryID, FolderEntryID)
			{
			}

			// Token: 0x060009ED RID: 2541 RVA: 0x00030C5C File Offset: 0x0002EE5C
			internal unsafe ExternalCopy(_Action* pact) : base(pact)
			{
			}
		}

		// Token: 0x02000237 RID: 567
		public class Reply : RuleAction.BaseReply
		{
			// Token: 0x17000109 RID: 265
			// (get) Token: 0x060009EE RID: 2542 RVA: 0x00030C65 File Offset: 0x0002EE65
			// (set) Token: 0x060009EF RID: 2543 RVA: 0x00030C6D File Offset: 0x0002EE6D
			public byte[] ReplyTemplateMessageEntryID
			{
				get
				{
					return this._ReplyTemplateMessageEntryID;
				}
				set
				{
					this._ReplyTemplateMessageEntryID = value;
				}
			}

			// Token: 0x1700010A RID: 266
			// (get) Token: 0x060009F0 RID: 2544 RVA: 0x00030C76 File Offset: 0x0002EE76
			// (set) Token: 0x060009F1 RID: 2545 RVA: 0x00030C7E File Offset: 0x0002EE7E
			public Guid ReplyTemplateGuid
			{
				get
				{
					return this._ReplyTemplateGuid;
				}
				set
				{
					this._ReplyTemplateGuid = value;
				}
			}

			// Token: 0x1700010B RID: 267
			// (get) Token: 0x060009F2 RID: 2546 RVA: 0x00030C87 File Offset: 0x0002EE87
			// (set) Token: 0x060009F3 RID: 2547 RVA: 0x00030C8F File Offset: 0x0002EE8F
			public RuleAction.Reply.ActionFlags Flags
			{
				get
				{
					return this._raf;
				}
				set
				{
					this._raf = value;
				}
			}

			// Token: 0x060009F4 RID: 2548 RVA: 0x00030C98 File Offset: 0x0002EE98
			public Reply(byte[] ReplyTemplateMessageEntryID, Guid ReplyTemplateGuid, RuleAction.Reply.ActionFlags Flags) : base(RuleAction.Type.OP_REPLY, ReplyTemplateMessageEntryID, ReplyTemplateGuid, Flags)
			{
			}

			// Token: 0x060009F5 RID: 2549 RVA: 0x00030CA4 File Offset: 0x0002EEA4
			internal unsafe Reply(_Action* pact) : base(pact)
			{
			}

			// Token: 0x02000238 RID: 568
			internal enum ActionFlags : uint
			{
				// Token: 0x04000FDF RID: 4063
				None,
				// Token: 0x04000FE0 RID: 4064
				DoNotSendToOriginator,
				// Token: 0x04000FE1 RID: 4065
				UseStockReplyTemplate
			}
		}

		// Token: 0x02000239 RID: 569
		public class OOFReply : RuleAction.BaseReply
		{
			// Token: 0x1700010C RID: 268
			// (get) Token: 0x060009F6 RID: 2550 RVA: 0x00030CAD File Offset: 0x0002EEAD
			// (set) Token: 0x060009F7 RID: 2551 RVA: 0x00030CB5 File Offset: 0x0002EEB5
			public byte[] ReplyTemplateMessageEntryID
			{
				get
				{
					return this._ReplyTemplateMessageEntryID;
				}
				set
				{
					this._ReplyTemplateMessageEntryID = value;
				}
			}

			// Token: 0x1700010D RID: 269
			// (get) Token: 0x060009F8 RID: 2552 RVA: 0x00030CBE File Offset: 0x0002EEBE
			// (set) Token: 0x060009F9 RID: 2553 RVA: 0x00030CC6 File Offset: 0x0002EEC6
			public Guid ReplyTemplateGuid
			{
				get
				{
					return this._ReplyTemplateGuid;
				}
				set
				{
					this._ReplyTemplateGuid = value;
				}
			}

			// Token: 0x060009FA RID: 2554 RVA: 0x00030CCF File Offset: 0x0002EECF
			public OOFReply(byte[] ReplyTemplateMessageEntryID, Guid ReplyTemplateGuid) : base(RuleAction.Type.OP_OOF_REPLY, ReplyTemplateMessageEntryID, ReplyTemplateGuid, RuleAction.Reply.ActionFlags.None)
			{
			}

			// Token: 0x060009FB RID: 2555 RVA: 0x00030CDB File Offset: 0x0002EEDB
			internal unsafe OOFReply(_Action* pact) : base(pact)
			{
			}
		}

		// Token: 0x0200023A RID: 570
		public class Defer : RuleAction
		{
			// Token: 0x1700010E RID: 270
			// (get) Token: 0x060009FC RID: 2556 RVA: 0x00030CE4 File Offset: 0x0002EEE4
			// (set) Token: 0x060009FD RID: 2557 RVA: 0x00030CEC File Offset: 0x0002EEEC
			public byte[] Data
			{
				get
				{
					return this._Data;
				}
				set
				{
					this._Data = value;
				}
			}

			// Token: 0x060009FE RID: 2558 RVA: 0x00030CF5 File Offset: 0x0002EEF5
			public Defer(byte[] Data) : base(RuleAction.Type.OP_DEFER_ACTION)
			{
				this._Data = Data;
			}

			// Token: 0x060009FF RID: 2559 RVA: 0x00030D08 File Offset: 0x0002EF08
			internal unsafe Defer(_Action* pact) : base(pact)
			{
				this._Data = new byte[pact->union.actDeferAction.cb];
				if (pact->union.actDeferAction.cb != 0)
				{
					Marshal.Copy((IntPtr)((void*)pact->union.actDeferAction.lpb), this._Data, 0, this._Data.Length);
				}
			}

			// Token: 0x06000A00 RID: 2560 RVA: 0x00030D74 File Offset: 0x0002EF74
			internal override int GetBytesToMarshal()
			{
				int num = 0;
				num += (_Action.SizeOf + 7 & -8);
				return num + (this._Data.Length + 7 & -8);
			}

			// Token: 0x06000A01 RID: 2561 RVA: 0x00030DA4 File Offset: 0x0002EFA4
			internal unsafe override void MarshalToNative(_Action* pact, ref byte* pExtra)
			{
				base.BaseMarshal(pact);
				byte* ptr = pExtra;
				pExtra += (IntPtr)(this._Data.Length + 7 & -8);
				pact->union.actDeferAction.cb = this._Data.Length;
				if (this._Data.Length != 0)
				{
					pact->union.actDeferAction.lpb = ptr;
					Marshal.Copy(this._Data, 0, (IntPtr)((void*)ptr), this._Data.Length);
					return;
				}
				pact->union.actDeferAction.lpb = null;
			}

			// Token: 0x04000FE2 RID: 4066
			private byte[] _Data;
		}

		// Token: 0x0200023B RID: 571
		public class Forward : RuleAction.FwdDelegate
		{
			// Token: 0x1700010F RID: 271
			// (get) Token: 0x06000A02 RID: 2562 RVA: 0x00030E2D File Offset: 0x0002F02D
			// (set) Token: 0x06000A03 RID: 2563 RVA: 0x00030E35 File Offset: 0x0002F035
			public RuleAction.Forward.ActionFlags Flags
			{
				get
				{
					return this._faf;
				}
				set
				{
					this._faf = value;
				}
			}

			// Token: 0x06000A04 RID: 2564 RVA: 0x00030E3E File Offset: 0x0002F03E
			public Forward(AdrEntry[] Recipients, RuleAction.Forward.ActionFlags Flags) : base(RuleAction.Type.OP_FORWARD, Recipients, Flags)
			{
			}

			// Token: 0x06000A05 RID: 2565 RVA: 0x00030E49 File Offset: 0x0002F049
			internal unsafe Forward(_Action* pact) : base(pact)
			{
			}

			// Token: 0x0200023C RID: 572
			internal enum ActionFlags : uint
			{
				// Token: 0x04000FE4 RID: 4068
				None,
				// Token: 0x04000FE5 RID: 4069
				PreserveSender,
				// Token: 0x04000FE6 RID: 4070
				DoNotMungeMessage,
				// Token: 0x04000FE7 RID: 4071
				ForwardAsAttachment = 4U,
				// Token: 0x04000FE8 RID: 4072
				SendSmsAlert = 8U
			}
		}

		// Token: 0x0200023D RID: 573
		public class Delegate : RuleAction.FwdDelegate
		{
			// Token: 0x06000A06 RID: 2566 RVA: 0x00030E52 File Offset: 0x0002F052
			public Delegate(AdrEntry[] Recipients) : base(RuleAction.Type.OP_DELEGATE, Recipients, RuleAction.Forward.ActionFlags.None)
			{
			}

			// Token: 0x06000A07 RID: 2567 RVA: 0x00030E5D File Offset: 0x0002F05D
			internal unsafe Delegate(_Action* pact) : base(pact)
			{
			}
		}

		// Token: 0x0200023E RID: 574
		public class Tag : RuleAction
		{
			// Token: 0x17000110 RID: 272
			// (get) Token: 0x06000A08 RID: 2568 RVA: 0x00030E66 File Offset: 0x0002F066
			// (set) Token: 0x06000A09 RID: 2569 RVA: 0x00030E6E File Offset: 0x0002F06E
			public PropValue Value
			{
				get
				{
					return this._value;
				}
				set
				{
					this._value = value;
				}
			}

			// Token: 0x06000A0A RID: 2570 RVA: 0x00030E77 File Offset: 0x0002F077
			public Tag(PropValue Value) : base(RuleAction.Type.OP_TAG)
			{
				this._value = Value;
			}

			// Token: 0x06000A0B RID: 2571 RVA: 0x00030E88 File Offset: 0x0002F088
			internal unsafe Tag(_Action* pact) : base(pact)
			{
				this._value = new PropValue(&pact->union.propTag);
			}

			// Token: 0x06000A0C RID: 2572 RVA: 0x00030EA8 File Offset: 0x0002F0A8
			internal override int GetBytesToMarshal()
			{
				int num = 0;
				num += (_Action.SizeOf + 7 & -8);
				return num + this._value.GetBytesToMarshal();
			}

			// Token: 0x06000A0D RID: 2573 RVA: 0x00030ED3 File Offset: 0x0002F0D3
			internal unsafe override void MarshalToNative(_Action* pact, ref byte* pExtra)
			{
				base.BaseMarshal(pact);
				this._value.MarshalToNative(&pact->union.propTag, ref pExtra);
			}

			// Token: 0x04000FE9 RID: 4073
			private PropValue _value;
		}

		// Token: 0x0200023F RID: 575
		public class Delete : RuleAction
		{
			// Token: 0x06000A0E RID: 2574 RVA: 0x00030EF4 File Offset: 0x0002F0F4
			public Delete() : base(RuleAction.Type.OP_DELETE)
			{
			}

			// Token: 0x06000A0F RID: 2575 RVA: 0x00030EFE File Offset: 0x0002F0FE
			internal unsafe Delete(_Action* pact) : base(pact)
			{
			}

			// Token: 0x06000A10 RID: 2576 RVA: 0x00030F08 File Offset: 0x0002F108
			internal override int GetBytesToMarshal()
			{
				int num = 0;
				return num + (_Action.SizeOf + 7 & -8);
			}

			// Token: 0x06000A11 RID: 2577 RVA: 0x00030F25 File Offset: 0x0002F125
			internal unsafe override void MarshalToNative(_Action* pact, ref byte* pExtra)
			{
				base.BaseMarshal(pact);
			}
		}

		// Token: 0x02000240 RID: 576
		public class MarkAsRead : RuleAction
		{
			// Token: 0x06000A12 RID: 2578 RVA: 0x00030F2E File Offset: 0x0002F12E
			public MarkAsRead() : base(RuleAction.Type.OP_MARK_AS_READ)
			{
			}

			// Token: 0x06000A13 RID: 2579 RVA: 0x00030F38 File Offset: 0x0002F138
			internal unsafe MarkAsRead(_Action* pact) : base(pact)
			{
			}

			// Token: 0x06000A14 RID: 2580 RVA: 0x00030F44 File Offset: 0x0002F144
			internal override int GetBytesToMarshal()
			{
				int num = 0;
				return num + (_Action.SizeOf + 7 & -8);
			}

			// Token: 0x06000A15 RID: 2581 RVA: 0x00030F61 File Offset: 0x0002F161
			internal unsafe override void MarshalToNative(_Action* pact, ref byte* pExtra)
			{
				base.BaseMarshal(pact);
			}
		}
	}
}
