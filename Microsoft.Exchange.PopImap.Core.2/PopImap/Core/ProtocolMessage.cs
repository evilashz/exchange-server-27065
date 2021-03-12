using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Exchange.Common.IL;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Mime.Internal;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.PopImap.Core
{
	// Token: 0x0200002F RID: 47
	internal abstract class ProtocolMessage : IComparable
	{
		// Token: 0x060002D2 RID: 722 RVA: 0x0000A7BE File Offset: 0x000089BE
		protected ProtocolMessage(int index, int imapId, int docId, int size)
		{
			this.index = index;
			this.id = imapId;
			this.documentId = docId;
			this.Size = size;
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x060002D3 RID: 723 RVA: 0x0000A7E3 File Offset: 0x000089E3
		// (set) Token: 0x060002D4 RID: 724 RVA: 0x0000A7EB File Offset: 0x000089EB
		public int Index
		{
			get
			{
				return this.index;
			}
			set
			{
				this.index = value;
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x060002D5 RID: 725 RVA: 0x0000A7F4 File Offset: 0x000089F4
		// (set) Token: 0x060002D6 RID: 726 RVA: 0x0000A7FC File Offset: 0x000089FC
		public int Id
		{
			get
			{
				return this.id;
			}
			set
			{
				this.id = value;
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060002D7 RID: 727
		// (set) Token: 0x060002D8 RID: 728
		public abstract bool IsNotRfc822Renderable { get; set; }

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060002D9 RID: 729 RVA: 0x0000A805 File Offset: 0x00008A05
		// (set) Token: 0x060002DA RID: 730 RVA: 0x0000A80D File Offset: 0x00008A0D
		public bool IsDeleted
		{
			get
			{
				return this.deleted;
			}
			set
			{
				this.deleted = value;
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060002DB RID: 731
		public abstract StoreObjectId Uid { get; }

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060002DC RID: 732
		public abstract ResponseFactory Factory { get; }

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x060002DD RID: 733 RVA: 0x0000A816 File Offset: 0x00008A16
		// (set) Token: 0x060002DE RID: 734 RVA: 0x0000A81E File Offset: 0x00008A1E
		internal int DocumentId
		{
			get
			{
				return this.documentId;
			}
			set
			{
				this.documentId = value;
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x060002DF RID: 735 RVA: 0x0000A827 File Offset: 0x00008A27
		// (set) Token: 0x060002E0 RID: 736 RVA: 0x0000A82F File Offset: 0x00008A2F
		internal int Size { get; private set; }

		// Token: 0x060002E1 RID: 737 RVA: 0x0000A838 File Offset: 0x00008A38
		public static Stream CreateMimeStream()
		{
			return new StreamWrapper();
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x0000A83F File Offset: 0x00008A3F
		public static MimeHeaderStream CreateMimeHeaderStream()
		{
			return new MimeHeaderStream();
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x0000A846 File Offset: 0x00008A46
		public static string Rfc2047Encode(string value, Encoding encoding)
		{
			return MimeInternalHelpers.Rfc2047Encode(value, encoding);
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x0000A84F File Offset: 0x00008A4F
		public override string ToString()
		{
			return string.Format("{0} {1}, user \"{2}\"", base.GetType().Name, this.id, this.Factory.UserName);
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x0000A87C File Offset: 0x00008A7C
		public int CompareTo(object obj)
		{
			ProtocolMessage protocolMessage = obj as ProtocolMessage;
			if (protocolMessage != null)
			{
				return this.Id - protocolMessage.Id;
			}
			throw new ArgumentException("object is not a ProtocolMessage");
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x0000A8AB File Offset: 0x00008AAB
		public override bool Equals(object obj)
		{
			return this.CompareTo(obj) == 0;
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x0000A8B7 File Offset: 0x00008AB7
		public override int GetHashCode()
		{
			return this.id.GetHashCode();
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x0000A8C4 File Offset: 0x00008AC4
		internal static void HandleMimeConversionException(Item item, ProtocolMessage msg, string subject, string date, Exception exception)
		{
			string arg = string.Format("{0}, subject: \"{1}\", date: \"{2}\"", msg, subject, date);
			if (exception is ObjectNotFoundException)
			{
				ProtocolBaseServices.SessionTracer.TraceDebug<string, Exception>(msg.Factory.Session.SessionId, "Exception in Mime Conversion for message {0}\r\n{1}", arg, exception);
			}
			else
			{
				ProtocolBaseServices.SessionTracer.TraceError<string, Exception>(msg.Factory.Session.SessionId, "Exception in Mime Conversion for message {0}\r\n{1}", arg, exception);
			}
			if (!(exception is StorageTransientException))
			{
				msg.IsNotRfc822Renderable = true;
				if (item != null)
				{
					ProtocolMessage.StampPoisonMessageFlag(item);
				}
			}
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x0000A964 File Offset: 0x00008B64
		internal static T ProcessMessageWithPoisonMessageHandling<T>(FilterDelegate filter, Func<T> processMessage)
		{
			ProtocolMessage.<>c__DisplayClass1<T> CS$<>8__locals1 = new ProtocolMessage.<>c__DisplayClass1<T>();
			CS$<>8__locals1.processMessage = processMessage;
			CS$<>8__locals1.t = default(T);
			ILUtil.DoTryFilterCatch(new TryDelegate(CS$<>8__locals1, (UIntPtr)ldftn(<ProcessMessageWithPoisonMessageHandling>b__0)), filter, null);
			return CS$<>8__locals1.t;
		}

		// Token: 0x060002EA RID: 746 RVA: 0x0000A9A4 File Offset: 0x00008BA4
		internal static void HandlePoisonMessageException(Exception e, Item item)
		{
			if (e is AccessViolationException || e is ArithmeticException || e is ArrayTypeMismatchException || e is ArgumentException || e is KeyNotFoundException || e is FormatException || e is IndexOutOfRangeException || e is InvalidCastException || e is InvalidOperationException || e is InvalidDataException || e is NotImplementedException || e is NotSupportedException || e is NullReferenceException)
			{
				ProtocolMessage.StampPoisonMessageFlag(item);
			}
		}

		// Token: 0x060002EB RID: 747 RVA: 0x0000AA20 File Offset: 0x00008C20
		internal static int GetDocIdFromInstanceKey(byte[] instanceKey)
		{
			if (instanceKey == null)
			{
				throw new ArgumentNullException("instanceKey");
			}
			if (instanceKey.Length != 20)
			{
				throw new ArgumentException("Invalid instance key:" + instanceKey.Length);
			}
			int num = instanceKey.Length;
			return (int)instanceKey[num - 4] | (int)instanceKey[num - 3] << 8 | (int)instanceKey[num - 2] << 16 | (int)instanceKey[num - 1] << 24;
		}

		// Token: 0x060002EC RID: 748 RVA: 0x0000AA80 File Offset: 0x00008C80
		private static void StampPoisonMessageFlag(Item item)
		{
			try
			{
				item.OpenAsReadWrite();
				item[MessageItemSchema.MimeConversionFailed] = true;
				item.Save(SaveMode.ResolveConflicts);
			}
			catch (LocalizedException)
			{
			}
		}

		// Token: 0x0400017F RID: 383
		private int index;

		// Token: 0x04000180 RID: 384
		private int id;

		// Token: 0x04000181 RID: 385
		private int documentId;

		// Token: 0x04000182 RID: 386
		private bool deleted;
	}
}
