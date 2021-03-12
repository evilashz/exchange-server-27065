using System;
using Microsoft.Ceres.External.ContentApi;
using Microsoft.Exchange.Search.Core.Abstraction;
using Microsoft.Exchange.Search.Core.Diagnostics;
using Microsoft.Exchange.Search.OperatorSchema;

namespace Microsoft.Exchange.Search.Fast
{
	// Token: 0x02000011 RID: 17
	internal class FastDocument : IFastDocument
	{
		// Token: 0x060000C4 RID: 196 RVA: 0x000059DD File Offset: 0x00003BDD
		internal FastDocument(IDiagnosticsSession diagnosticsSession, string contextId, DocumentOperation operation)
		{
			this.diagnosticsSession = diagnosticsSession;
			this.document = new Document(contextId, FastDocument.GetFastOperation(operation));
			this.correlationId = Guid.NewGuid();
			this.SetGuid("CorrelationId", this.correlationId);
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x00005A1A File Offset: 0x00003C1A
		// (set) Token: 0x060000C6 RID: 198 RVA: 0x00005A22 File Offset: 0x00003C22
		public int AttemptCount
		{
			get
			{
				return this.attemptCount;
			}
			set
			{
				this.SetInteger(FastIndexSystemSchema.AttemptCount.Name, value);
				this.attemptCount = value;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x00005A3C File Offset: 0x00003C3C
		public Document Document
		{
			get
			{
				return this.document;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x00005A44 File Offset: 0x00003C44
		// (set) Token: 0x060000C9 RID: 201 RVA: 0x00005A4C File Offset: 0x00003C4C
		public string CompositeItemId
		{
			get
			{
				return this.compositeItemId;
			}
			set
			{
				this.SetString(FastIndexSystemSchema.ItemId.Name, value);
				this.compositeItemId = value;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000CA RID: 202 RVA: 0x00005A66 File Offset: 0x00003C66
		public string ContextId
		{
			get
			{
				return this.document.DocumentId;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000CB RID: 203 RVA: 0x00005A73 File Offset: 0x00003C73
		public Guid CorrelationId
		{
			get
			{
				return this.correlationId;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000CC RID: 204 RVA: 0x00005A7B File Offset: 0x00003C7B
		// (set) Token: 0x060000CD RID: 205 RVA: 0x00005A83 File Offset: 0x00003C83
		public long DocumentId
		{
			get
			{
				return this.documentId;
			}
			set
			{
				this.SetLong(FastIndexSystemSchema.DocumentId.Name, value);
				this.documentId = value;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000CE RID: 206 RVA: 0x00005A9D File Offset: 0x00003C9D
		// (set) Token: 0x060000CF RID: 207 RVA: 0x00005AB9 File Offset: 0x00003CB9
		public int ErrorCode
		{
			get
			{
				if (this.errorCode == null)
				{
					return 0;
				}
				return this.errorCode.Value;
			}
			set
			{
				this.errorCode = new int?(value);
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x00005AC7 File Offset: 0x00003CC7
		// (set) Token: 0x060000D1 RID: 209 RVA: 0x00005ACF File Offset: 0x00003CCF
		public string ErrorMessage
		{
			get
			{
				return this.errorMessage;
			}
			set
			{
				this.SetString("errormessage", value);
				this.errorMessage = value;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x00005AE4 File Offset: 0x00003CE4
		// (set) Token: 0x060000D3 RID: 211 RVA: 0x00005AEC File Offset: 0x00003CEC
		public int FeedingVersion
		{
			get
			{
				return this.feedingVersion;
			}
			set
			{
				this.SetInteger(FastIndexSystemSchema.Version.Name, value);
				this.feedingVersion = value;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x00005B06 File Offset: 0x00003D06
		// (set) Token: 0x060000D5 RID: 213 RVA: 0x00005B0E File Offset: 0x00003D0E
		public string FlowOperation
		{
			get
			{
				return this.flowOperation;
			}
			set
			{
				this.SetString("ExchangeCtsFlowOperation", value);
				this.flowOperation = value;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x00005B23 File Offset: 0x00003D23
		// (set) Token: 0x060000D7 RID: 215 RVA: 0x00005B2B File Offset: 0x00003D2B
		public string FolderId
		{
			get
			{
				return this.folderId;
			}
			set
			{
				this.SetString(FastIndexSystemSchema.FolderId.Name, value);
				this.folderId = value;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x00005B45 File Offset: 0x00003D45
		// (set) Token: 0x060000D9 RID: 217 RVA: 0x00005B4D File Offset: 0x00003D4D
		public long IndexId
		{
			get
			{
				return this.indexId;
			}
			set
			{
				this.SetLong("indexid", value);
				this.indexId = value;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000DA RID: 218 RVA: 0x00005B62 File Offset: 0x00003D62
		// (set) Token: 0x060000DB RID: 219 RVA: 0x00005B6A File Offset: 0x00003D6A
		public string IndexSystemName
		{
			get
			{
				return this.indexSystemName;
			}
			set
			{
				this.SetString("indexsystemname", value);
				this.indexSystemName = value;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000DC RID: 220 RVA: 0x00005B7F File Offset: 0x00003D7F
		// (set) Token: 0x060000DD RID: 221 RVA: 0x00005B87 File Offset: 0x00003D87
		public string InstanceName
		{
			get
			{
				return this.instanceName;
			}
			set
			{
				this.SetString("instancename", value);
				this.instanceName = value;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000DE RID: 222 RVA: 0x00005B9C File Offset: 0x00003D9C
		// (set) Token: 0x060000DF RID: 223 RVA: 0x00005BA4 File Offset: 0x00003DA4
		public bool IsLocalMdb
		{
			get
			{
				return this.isLocalMdb;
			}
			set
			{
				this.SetBool("islocalmdb", value);
				this.isLocalMdb = value;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x00005BB9 File Offset: 0x00003DB9
		// (set) Token: 0x060000E1 RID: 225 RVA: 0x00005BC1 File Offset: 0x00003DC1
		public bool IsMoveDestination
		{
			get
			{
				return this.isMoveDestination;
			}
			set
			{
				this.SetBool("ismovedestination", value);
				this.isMoveDestination = value;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x00005BD6 File Offset: 0x00003DD6
		// (set) Token: 0x060000E3 RID: 227 RVA: 0x00005BDE File Offset: 0x00003DDE
		public Guid MailboxGuid
		{
			get
			{
				return this.mailboxGuid;
			}
			set
			{
				this.SetString(FastIndexSystemSchema.MailboxGuid.Name, value.ToString());
				this.mailboxGuid = value;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x00005C04 File Offset: 0x00003E04
		// (set) Token: 0x060000E5 RID: 229 RVA: 0x00005C0C File Offset: 0x00003E0C
		public int Port
		{
			get
			{
				return this.port;
			}
			set
			{
				this.SetInteger("port", value);
				this.port = value;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x00005C21 File Offset: 0x00003E21
		// (set) Token: 0x060000E7 RID: 231 RVA: 0x00005C29 File Offset: 0x00003E29
		public int MessageFlags
		{
			get
			{
				return this.messageFlags;
			}
			set
			{
				this.SetInteger("messageflags", value);
				this.messageFlags = value;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x00005C3E File Offset: 0x00003E3E
		// (set) Token: 0x060000E9 RID: 233 RVA: 0x00005C46 File Offset: 0x00003E46
		public Guid TenantId
		{
			get
			{
				return this.tenantId;
			}
			set
			{
				this.SetGuid("tenantid", value);
				this.tenantId = value;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000EA RID: 234 RVA: 0x00005C5B File Offset: 0x00003E5B
		// (set) Token: 0x060000EB RID: 235 RVA: 0x00005C63 File Offset: 0x00003E63
		public bool Tracked { get; set; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000EC RID: 236 RVA: 0x00005C6C File Offset: 0x00003E6C
		// (set) Token: 0x060000ED RID: 237 RVA: 0x00005C74 File Offset: 0x00003E74
		public string TransportContextId
		{
			get
			{
				return this.transportContextId;
			}
			set
			{
				this.SetString("contextid", value);
				this.transportContextId = value;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000EE RID: 238 RVA: 0x00005C89 File Offset: 0x00003E89
		// (set) Token: 0x060000EF RID: 239 RVA: 0x00005C91 File Offset: 0x00003E91
		public long Watermark
		{
			get
			{
				return this.watermark;
			}
			set
			{
				this.SetLong(FastIndexSystemSchema.Watermark.Name, value);
				this.watermark = value;
			}
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00005CAB File Offset: 0x00003EAB
		public void PrepareForSubmit()
		{
			this.SetDateTime("SubmitTime", DateTime.UtcNow);
			if (this.errorCode != null)
			{
				this.SetInteger(FastIndexSystemSchema.ErrorCode.Name, this.ErrorCode);
			}
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00005CE0 File Offset: 0x00003EE0
		private static Operation GetFastOperation(DocumentOperation operation)
		{
			Operation result;
			switch (operation)
			{
			case DocumentOperation.Insert:
				result = Operation.Insert;
				break;
			case DocumentOperation.Update:
			case DocumentOperation.Move:
				result = Operation.Update;
				break;
			case DocumentOperation.Delete:
				result = Operation.Delete;
				break;
			case DocumentOperation.DeleteSelection:
				result = Operation.DeleteSelection;
				break;
			default:
				throw new NotSupportedException("This document operation is not supported");
			}
			return result;
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00005D37 File Offset: 0x00003F37
		private void SetBool(string name, bool value)
		{
			this.diagnosticsSession.TraceDebug<string, string, bool>("Document {0}, {1}={2}", this.ContextId, name, value);
			this.document.SetBool(name, new bool?(value));
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00005D63 File Offset: 0x00003F63
		private void SetLong(string name, long value)
		{
			this.diagnosticsSession.TraceDebug<string, string, long>("Document {0}, {1}={2}", this.ContextId, name, value);
			this.document.SetLong(name, new long?(value));
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00005D8F File Offset: 0x00003F8F
		private void SetInteger(string name, int value)
		{
			this.diagnosticsSession.TraceDebug<string, string, int>("Document {0}, {1}={2}", this.ContextId, name, value);
			this.document.SetInteger(name, new int?(value));
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00005DBB File Offset: 0x00003FBB
		private void SetString(string name, string value)
		{
			this.diagnosticsSession.TraceDebug<string, string, string>("Document {0}, {1}={2}", this.ContextId, name, value);
			this.document.SetString(name, value);
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00005DE2 File Offset: 0x00003FE2
		private void SetGuid(string name, Guid value)
		{
			this.diagnosticsSession.TraceDebug<string, string, Guid>("Document {0}, {1}={2}", this.ContextId, name, value);
			this.document.SetGuid(name, value);
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00005E09 File Offset: 0x00004009
		private void SetDateTime(string name, DateTime value)
		{
			this.diagnosticsSession.TraceDebug<string, string, DateTime>("Document {0}, {1}={2}", this.ContextId, name, value);
			this.document.SetDateTime(name, new DateTime?(value));
		}

		// Token: 0x0400004F RID: 79
		private readonly Document document;

		// Token: 0x04000050 RID: 80
		private readonly Guid correlationId;

		// Token: 0x04000051 RID: 81
		private readonly IDiagnosticsSession diagnosticsSession;

		// Token: 0x04000052 RID: 82
		private int attemptCount;

		// Token: 0x04000053 RID: 83
		private string compositeItemId;

		// Token: 0x04000054 RID: 84
		private long documentId;

		// Token: 0x04000055 RID: 85
		private int? errorCode;

		// Token: 0x04000056 RID: 86
		private string errorMessage;

		// Token: 0x04000057 RID: 87
		private int feedingVersion;

		// Token: 0x04000058 RID: 88
		private string flowOperation;

		// Token: 0x04000059 RID: 89
		private string folderId;

		// Token: 0x0400005A RID: 90
		private long indexId;

		// Token: 0x0400005B RID: 91
		private string indexSystemName;

		// Token: 0x0400005C RID: 92
		private string instanceName;

		// Token: 0x0400005D RID: 93
		private bool isLocalMdb;

		// Token: 0x0400005E RID: 94
		private bool isMoveDestination;

		// Token: 0x0400005F RID: 95
		private Guid mailboxGuid;

		// Token: 0x04000060 RID: 96
		private int port;

		// Token: 0x04000061 RID: 97
		private int messageFlags;

		// Token: 0x04000062 RID: 98
		private Guid tenantId;

		// Token: 0x04000063 RID: 99
		private string transportContextId;

		// Token: 0x04000064 RID: 100
		private long watermark;
	}
}
