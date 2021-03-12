using System;
using System.IO;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.PopImap.Core;

namespace Microsoft.Exchange.Imap4
{
	// Token: 0x02000031 RID: 49
	internal class Imap4RequestAppend : Imap4RequestWithStringParameters, IDisposeTrackable, IDisposable
	{
		// Token: 0x060001EC RID: 492 RVA: 0x0000D084 File Offset: 0x0000B284
		public Imap4RequestAppend(Imap4ResponseFactory factory, string tag, string data) : base(factory, tag, data, 1, 3)
		{
			this.PerfCounterTotal = base.Imap4CountersInstance.PerfCounter_APPEND_Total;
			this.PerfCounterFailures = base.Imap4CountersInstance.PerfCounter_APPEND_Failures;
			base.AllowedStates = (Imap4State.Authenticated | Imap4State.Selected);
			this.disposeTracker = this.GetDisposeTracker();
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060001ED RID: 493 RVA: 0x0000D0D1 File Offset: 0x0000B2D1
		protected override int StoredDataLength
		{
			get
			{
				if (this.additionalBytes == null)
				{
					return 0;
				}
				return this.additionalBytes.Length;
			}
		}

		// Token: 0x060001EE RID: 494 RVA: 0x0000D0E8 File Offset: 0x0000B2E8
		public override void ParseArguments()
		{
			int count = base.ArrayOfArguments.Count;
			if (this.additionalBytes != null)
			{
				if (!string.IsNullOrEmpty(base.Arguments))
				{
					throw new ArgumentException("this.Arguments is not empty and it's about to be overriden.");
				}
				base.Arguments = this.additionalBytes.ToString();
			}
			base.ParseArguments();
			if (base.NextLiteralSize > 0)
			{
				if (base.NextLiteralSize > base.Factory.MaxReceiveSize)
				{
					ProtocolBaseServices.SessionTracer.TraceDebug<int>(base.Session.SessionId, "Literal size is too big {0}, skip the data.", base.NextLiteralSize);
					this.ParseResult = ParseResult.invalidArgument;
					return;
				}
				this.literalReceived = true;
				if (this.additionalBytes != null)
				{
					this.additionalBytes.Reset();
				}
			}
			if (!this.literalReceived || (base.NextLiteralSize == 0 && count < base.ArrayOfArguments.Count - 1))
			{
				this.ParseResult = ParseResult.invalidNumberOfArguments;
			}
		}

		// Token: 0x060001EF RID: 495 RVA: 0x0000D1C0 File Offset: 0x0000B3C0
		public override void AddData(byte[] data, int offset, int dataLength, out int bytesConsumed)
		{
			ProtocolBaseServices.SessionTracer.TraceDebug<int, int>(base.Session.SessionId, "NextLiteralSize {0} ArrayOfArguments.Length {1}.", base.NextLiteralSize, base.ArrayOfArguments.Count);
			bytesConsumed = 0;
			int num = offset;
			while (!this.IsComplete && bytesConsumed < dataLength)
			{
				this.skipData = this.ShouldSkipData();
				if (!this.skipData && this.mimeStream == null && (base.ArrayOfArguments.Count == base.MaxNumberOfArguments || base.NextLiteralSize > base.Factory.Session.Server.MaxCommandLength))
				{
					this.mimeStream = ProtocolMessage.CreateMimeStream();
				}
				if (this.mimeStream == null && !this.skipData)
				{
					ProtocolBaseServices.SessionTracer.TraceDebug(base.Session.SessionId, "Parsing additional arguments.");
					num += this.AddOneLine(data, num, dataLength - (num - offset), ref bytesConsumed);
				}
				else
				{
					int num2 = Math.Min(dataLength - bytesConsumed, base.NextLiteralSize);
					if (num2 > 0)
					{
						this.ReceiveMimeStream(data, num, num2, ref bytesConsumed);
						num += num2;
					}
					else
					{
						this.ReceiveLastCrLf(data, num, dataLength - (num - offset), ref bytesConsumed);
					}
				}
			}
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x0000D2E0 File Offset: 0x0000B4E0
		public override ProtocolResponse Process()
		{
			ProtocolResponse result;
			try
			{
				int num = base.ArrayOfArguments.Count;
				if (this.skipData)
				{
					if (num > base.MaxNumberOfArguments)
					{
						result = new Imap4Response(this, Imap4Response.Type.bad, "Command Argument Error. 12");
					}
					else
					{
						result = new Imap4Response(this, Imap4Response.Type.bad, "Command Argument Error. 11");
					}
				}
				else
				{
					if (this.mimeStream == null)
					{
						if (num < 2)
						{
							return new Imap4Response(this, Imap4Response.Type.bad, "Command Argument Error. 12");
						}
						this.mimeStream = ProtocolMessage.CreateMimeStream();
						this.mimeStream.Write(this.additionalBytes.GetBuffer(), 0, this.additionalBytes.Length);
						num--;
					}
					if (num == 0 || num > base.MaxNumberOfArguments)
					{
						result = new Imap4Response(this, Imap4Response.Type.bad, "Command Argument Error. 12");
					}
					else
					{
						Imap4Flags imap4Flags = Imap4Flags.None;
						ExDateTime exDateTime = ResponseFactory.CurrentExTimeZone.ConvertDateTime(ExDateTime.UtcNow);
						string path;
						Imap4Mailbox imap4Mailbox;
						if (!Imap4Mailbox.TryNormalizeMailboxPath(base.ArrayOfArguments[0], out path))
						{
							result = new Imap4Response(this, Imap4Response.Type.bad, "Command Argument Error. 11");
						}
						else if (!((Imap4Session)base.Factory.Session).TryGetMailbox(path, out imap4Mailbox, false))
						{
							result = new Imap4Response(this, Imap4Response.Type.no, "Folder is not found.")
							{
								ResponseCodes = "[TRYCREATE]"
							};
						}
						else if (imap4Mailbox.IsNonselect)
						{
							result = new Imap4Response(this, Imap4Response.Type.bad, "Command Argument Error. 11");
						}
						else
						{
							bool flag = false;
							if ((num == 3 && (!Imap4FlagsHelper.TryParse(base.ArrayOfArguments[1], out imap4Flags, out flag) || !Rfc822Date.TryParse(base.ArrayOfArguments[2], out exDateTime))) || (num == 2 && !(Imap4FlagsHelper.TryParse(base.ArrayOfArguments[1], out imap4Flags, out flag) | Rfc822Date.TryParse(base.ArrayOfArguments[1], out exDateTime))))
							{
								result = new Imap4Response(this, Imap4Response.Type.bad, "Command Argument Error. 11");
							}
							else if ((imap4Flags & Imap4Flags.Recent) != Imap4Flags.None)
							{
								result = new Imap4Response(this, Imap4Response.Type.bad, "Command Argument Error. 11");
							}
							else
							{
								this.mimeStream.Seek(0L, SeekOrigin.Begin);
								InboundConversionOptions inboundConversionOptions = base.Factory.GetInboundConversionOptions();
								inboundConversionOptions.ClearCategories = false;
								int num2;
								using (MessageItem messageItem = MessageItem.Create(base.Factory.Store, imap4Mailbox.Uid))
								{
									try
									{
										ItemConversion.ConvertAnyMimeToItem(messageItem, this.mimeStream, inboundConversionOptions);
										messageItem.Load(Imap4Message.MessageProperties);
									}
									catch (NotSupportedException exception)
									{
										return this.ProcessException(exception);
									}
									catch (NotImplementedException exception2)
									{
										return this.ProcessException(exception2);
									}
									catch (StoragePermanentException exception3)
									{
										return this.ProcessException(exception3);
									}
									catch (StorageTransientException exception4)
									{
										return this.ProcessException(exception4);
									}
									messageItem[MessageItemSchema.ClientSubmittedSecurely] = base.Factory.Session.IsTls;
									messageItem[ItemSchema.ImapInternalDate] = exDateTime;
									messageItem[ItemSchema.ReceivedTime] = exDateTime;
									messageItem[ItemSchema.ImapAppendStamp] = string.Format("{0} ({1})", ProtocolBaseServices.ServerName, ProtocolBaseServices.ServerVersion);
									Imap4FlagsHelper.Apply(messageItem, imap4Flags);
									try
									{
										ConflictResolutionResult conflictResolutionResult = messageItem.Save(SaveMode.ResolveConflicts);
										if (conflictResolutionResult.SaveStatus != SaveResult.Success)
										{
											throw new ApplicationException("Item saved with conflicts!");
										}
										messageItem.Load(Imap4Message.MessageProperties);
										num2 = (int)messageItem[ItemSchema.ImapId];
									}
									catch (CorruptDataException exception5)
									{
										return this.ProcessException(exception5);
									}
									catch (StoragePermanentException exception6)
									{
										return this.ProcessException(exception6);
									}
									catch (StorageTransientException exception7)
									{
										return this.ProcessException(exception7);
									}
									if (imap4Mailbox.Equals(base.Factory.SelectedMailbox))
									{
										base.Factory.SelectedMailbox.AddMessage((int)messageItem[ItemSchema.ImapId], (int)messageItem[ItemSchema.DocumentId], Imap4FlagsHelper.Parse(messageItem), (int)messageItem[ItemSchema.Size]);
									}
									if (base.Session.LightLogSession != null)
									{
										base.Session.LightLogSession.TotalSize = new long?(this.mimeStream.Length);
									}
								}
								Imap4Response imap4Response = flag ? new Imap4Response(this, Imap4Response.Type.ok, "* NO Keywords are not supported!\r\n[*] APPEND completed.") : new Imap4Response(this, Imap4Response.Type.ok, "APPEND completed.");
								if (base.SupportUidPlus)
								{
									imap4Response.ResponseCodes = string.Format("[APPENDUID {0} {1}]", imap4Mailbox.UidValidity, num2);
								}
								result = imap4Response;
							}
						}
					}
				}
			}
			finally
			{
				if (this.mimeStream != null)
				{
					this.mimeStream.Close();
					this.mimeStream = null;
				}
			}
			return result;
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x0000D820 File Offset: 0x0000BA20
		public virtual DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<Imap4RequestAppend>(this);
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x0000D828 File Offset: 0x0000BA28
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x0000D83D File Offset: 0x0000BA3D
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x0000D84C File Offset: 0x0000BA4C
		protected override void AppendArray(byte[] data, int offset, int dataLength)
		{
			if (this.additionalBytes == null)
			{
				this.additionalBytes = new BufferBuilder(dataLength);
			}
			this.additionalBytes.Append(data, offset, dataLength);
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x0000D870 File Offset: 0x0000BA70
		protected override void RemoveFromArgumentsString(int charsToRemove)
		{
			if (charsToRemove > this.additionalBytes.Length)
			{
				throw new ArgumentException("charsToRemove > this.additionalBytes.Length");
			}
			this.additionalBytes.SetLength(this.additionalBytes.Length - charsToRemove);
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x0000D8A3 File Offset: 0x0000BAA3
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && this.disposeTracker != null)
			{
				this.disposeTracker.Dispose();
				this.disposeTracker = null;
			}
			if (this.mimeStream != null)
			{
				this.mimeStream.Close();
				this.mimeStream = null;
			}
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x0000D8DC File Offset: 0x0000BADC
		private ProtocolResponse ProcessException(Exception exception)
		{
			base.Factory.LogHandledException(exception);
			return new Imap4Response(this, Imap4Response.Type.bad, "Command Argument Error. 11");
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x0000D8F8 File Offset: 0x0000BAF8
		private bool ShouldSkipData()
		{
			if (base.NextLiteralSize > base.Factory.MaxReceiveSize)
			{
				ProtocolBaseServices.SessionTracer.TraceDebug<int>(base.Session.SessionId, "Literal size is too big {0}, skip the data.", base.NextLiteralSize);
				return true;
			}
			if (base.ArrayOfArguments.Count > base.MaxNumberOfArguments)
			{
				ProtocolBaseServices.SessionTracer.TraceDebug<int>(base.Session.SessionId, "Too many arguments {0} skip the data.", base.ArrayOfArguments.Count);
				return true;
			}
			return false;
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x0000D978 File Offset: 0x0000BB78
		private void ReceiveMimeStream(byte[] data, int offset, int dataLength, ref int bytesConsumed)
		{
			if (this.skipData)
			{
				ProtocolBaseServices.SessionTracer.TraceDebug<int>(base.Session.SessionId, "Skipping data {0} bytes.", dataLength);
			}
			else
			{
				ProtocolBaseServices.SessionTracer.TraceDebug<int, int>(base.Session.SessionId, "Add data to mime stream {0} bytes of {1} expected.", dataLength, base.NextLiteralSize);
				this.mimeStream.Write(data, offset, dataLength);
			}
			bytesConsumed += dataLength;
			base.NextLiteralSize -= dataLength;
		}

		// Token: 0x060001FA RID: 506 RVA: 0x0000D9F0 File Offset: 0x0000BBF0
		private void ReceiveLastCrLf(byte[] data, int offset, int dataLength, ref int bytesConsumed)
		{
			if (dataLength >= 2)
			{
				if (data[offset] == 13 && data[offset + 1] == 10)
				{
					this.IsComplete = true;
					bytesConsumed += 2;
					return;
				}
				this.ParseResult = ParseResult.invalidArgument;
				this.IsComplete = true;
				return;
			}
			else
			{
				if (this.additionalBytes == null || this.additionalBytes.Length != 1)
				{
					if (dataLength == 1)
					{
						this.AppendArray(data, offset, 1);
						bytesConsumed++;
					}
					return;
				}
				if (this.additionalBytes.GetBuffer()[0] == 13 && data[offset] == 10)
				{
					this.IsComplete = true;
					bytesConsumed++;
					return;
				}
				this.ParseResult = ParseResult.invalidArgument;
				this.IsComplete = true;
				return;
			}
		}

		// Token: 0x0400016F RID: 367
		private const string AppendResponseCompleted = "APPEND completed.";

		// Token: 0x04000170 RID: 368
		private const string AppendResponseNotFound = "Folder is not found.";

		// Token: 0x04000171 RID: 369
		private const string AppendResponseNoToKeyword = "* NO Keywords are not supported!\r\n[*] APPEND completed.";

		// Token: 0x04000172 RID: 370
		private bool skipData;

		// Token: 0x04000173 RID: 371
		private bool literalReceived;

		// Token: 0x04000174 RID: 372
		private Stream mimeStream;

		// Token: 0x04000175 RID: 373
		private BufferBuilder additionalBytes;

		// Token: 0x04000176 RID: 374
		private DisposeTracker disposeTracker;
	}
}
