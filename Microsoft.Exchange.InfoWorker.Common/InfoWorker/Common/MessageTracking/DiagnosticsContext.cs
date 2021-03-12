using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Logging.Search;

namespace Microsoft.Exchange.InfoWorker.Common.MessageTracking
{
	// Token: 0x0200029A RID: 666
	internal class DiagnosticsContext
	{
		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x0600128F RID: 4751 RVA: 0x00055C54 File Offset: 0x00053E54
		public DiagnosticsLevel DiagnosticsLevel
		{
			get
			{
				return this.diagnosticsLevel;
			}
		}

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x06001290 RID: 4752 RVA: 0x00055C5C File Offset: 0x00053E5C
		public bool Enabled
		{
			get
			{
				return this.diagnosticsLevel != DiagnosticsLevel.None;
			}
		}

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x06001291 RID: 4753 RVA: 0x00055C6A File Offset: 0x00053E6A
		public bool VerboseDiagnostics
		{
			get
			{
				return this.diagnosticsLevel == DiagnosticsLevel.Verbose || this.diagnosticsLevel == DiagnosticsLevel.Etw;
			}
		}

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x06001292 RID: 4754 RVA: 0x00055C80 File Offset: 0x00053E80
		private int MaxEvents
		{
			get
			{
				if (this.VerboseDiagnostics)
				{
					return Math.Max(8192, ServerCache.Instance.MaxDiagnosticsEvents);
				}
				return ServerCache.Instance.MaxDiagnosticsEvents;
			}
		}

		// Token: 0x06001293 RID: 4755 RVA: 0x00055CAC File Offset: 0x00053EAC
		public DiagnosticsContext(bool suppressIdAllocation, DiagnosticsLevel diagnosticsLevel)
		{
			this.diagnosticsLevel = diagnosticsLevel;
			if (suppressIdAllocation)
			{
				return;
			}
			while (this.requestId == 0)
			{
				this.requestId = Interlocked.Increment(ref DiagnosticsContext.requestIdCounter);
			}
		}

		// Token: 0x06001294 RID: 4756 RVA: 0x00055D04 File Offset: 0x00053F04
		public void AddProperty(DiagnosticProperty property, object value)
		{
			if (!this.Enabled || value == null)
			{
				return;
			}
			if (this.currentEvent == null)
			{
				this.currentEvent = new List<KeyValuePair<string, object>>(5);
				this.InsertWellknownFields(this.currentEvent);
			}
			this.currentEvent.Add(new KeyValuePair<string, object>(Names<DiagnosticProperty>.Map[(int)property], value));
		}

		// Token: 0x06001295 RID: 4757 RVA: 0x00055D58 File Offset: 0x00053F58
		public void MergeEvents(string[] events)
		{
			if (!this.Enabled || events == null || events.Length == 0)
			{
				return;
			}
			int num = this.MaxEvents - this.data.Count;
			string text = Names<DiagnosticProperty>.Map[1];
			string text2 = Names<DiagnosticProperty>.Map[0];
			if (num > 1)
			{
				bool flag = false;
				int num2 = Math.Min(events.Length, num - 1);
				for (int i = 0; i < num2; i++)
				{
					if (string.IsNullOrEmpty(events[i]))
					{
						TraceWrapper.SearchLibraryTracer.TraceError(this.GetHashCode(), "Null or empty op-trace was unexpected", new object[0]);
					}
					else
					{
						byte[] bytes = Encoding.UTF8.GetBytes(events[i]);
						object obj = DiagnosticsContext.csvRowBuffer.ResetAndDecode(bytes, new CsvDecoderCallback(DiagnosticsContext.decoder.Decode));
						KeyValuePair<string, object>[] array = obj as KeyValuePair<string, object>[];
						if (array != null && array.Length > 2 && array[0].Value is int && array[1].Value is int)
						{
							if ((!StringComparer.InvariantCulture.Equals(array[0].Key, text2) || !StringComparer.InvariantCulture.Equals(array[1].Key, text)) && !flag)
							{
								TraceWrapper.SearchLibraryTracer.TraceError<string>(this.GetHashCode(), "Reversed Rts and Id, may be from Ex2010 or a new bug. OpTrace: {0}", events[i]);
								flag = true;
							}
							int num3 = this.lastRts + (int)array[0].Value;
							array[0] = new KeyValuePair<string, object>(text2, num3);
							array[1] = new KeyValuePair<string, object>(text, this.requestId);
							this.data.Add(array);
							num--;
						}
						else
						{
							string formatString = string.Format("Malformed OpTrace data, could not be decoded or used. OpTrace: {0}", events[i]);
							TraceWrapper.SearchLibraryTracer.TraceError(this.GetHashCode(), formatString, new object[0]);
						}
					}
				}
			}
			if (num == 1)
			{
				this.data.Add(this.GetTruncationEvent());
			}
		}

		// Token: 0x06001296 RID: 4758 RVA: 0x00055F60 File Offset: 0x00054160
		public void WriteEvent()
		{
			if (!this.Enabled)
			{
				return;
			}
			if (this.data.Count == this.MaxEvents)
			{
				return;
			}
			if (this.data.Count == this.MaxEvents - 1)
			{
				this.data.Add(this.GetTruncationEvent());
				return;
			}
			this.data.Add(this.currentEvent);
			this.currentEvent = null;
		}

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x06001297 RID: 4759 RVA: 0x00055FC9 File Offset: 0x000541C9
		public List<ICollection<KeyValuePair<string, object>>> Data
		{
			get
			{
				return this.data;
			}
		}

		// Token: 0x06001298 RID: 4760 RVA: 0x00055FD4 File Offset: 0x000541D4
		public string[] Serialize()
		{
			if (!this.Enabled || this.data == null)
			{
				return null;
			}
			string[] array = new string[this.data.Count];
			int num = 0;
			bool flag = false;
			foreach (IEnumerable<KeyValuePair<string, object>> enumerable in this.data)
			{
				array[num] = LogRowFormatter.FormatCollection(enumerable, out flag);
				if (flag)
				{
					byte[] array2 = Utf8Csv.EncodeAndEscape(array[num]);
					if (array2 == null)
					{
						return null;
					}
					array[num] = Encoding.UTF8.GetString(array2);
				}
				num++;
			}
			return array;
		}

		// Token: 0x06001299 RID: 4761 RVA: 0x00056080 File Offset: 0x00054280
		public void LogRpcStart(string server, RpcReason rpcReason)
		{
			this.AddProperty(DiagnosticProperty.Op, Names<Operations>.Map[0]);
			this.AddProperty(DiagnosticProperty.OpType, Names<OpType>.Map[0]);
			this.AddProperty(DiagnosticProperty.Svr, server);
			if (rpcReason != RpcReason.None)
			{
				this.AddProperty(DiagnosticProperty.Data1, Names<RpcReason>.Map[(int)rpcReason]);
			}
			this.WriteEvent();
		}

		// Token: 0x0600129A RID: 4762 RVA: 0x000560C0 File Offset: 0x000542C0
		public void LogRpcEnd(Exception error, int count)
		{
			this.AddProperty(DiagnosticProperty.Op, Names<Operations>.Map[0]);
			this.AddProperty(DiagnosticProperty.OpType, Names<OpType>.Map[1]);
			this.AddProperty(DiagnosticProperty.Cnt, count);
			if (error != null)
			{
				this.AddProperty(DiagnosticProperty.Err, error.GetType().Name);
			}
			this.WriteEvent();
		}

		// Token: 0x0600129B RID: 4763 RVA: 0x00056114 File Offset: 0x00054314
		private ICollection<KeyValuePair<string, object>> GetTruncationEvent()
		{
			List<KeyValuePair<string, object>> list = new List<KeyValuePair<string, object>>(3);
			this.InsertWellknownFields(list);
			list.Add(DiagnosticsContext.truncationProperty);
			return list;
		}

		// Token: 0x0600129C RID: 4764 RVA: 0x0005613C File Offset: 0x0005433C
		private void InsertWellknownFields(List<KeyValuePair<string, object>> eventData)
		{
			if (!this.Enabled)
			{
				return;
			}
			long num = DateTime.UtcNow.ToFileTime() - this.startTime;
			if (num < 0L)
			{
				num = 0L;
			}
			int num2 = (int)num;
			eventData.Add(new KeyValuePair<string, object>(Names<DiagnosticProperty>.Map[0], num2));
			eventData.Add(new KeyValuePair<string, object>(Names<DiagnosticProperty>.Map[1], this.requestId));
			eventData.Add(new KeyValuePair<string, object>(Names<DiagnosticProperty>.Map[2], Environment.CurrentManagedThreadId));
			this.lastRts = num2;
		}

		// Token: 0x04000C77 RID: 3191
		private static int requestIdCounter;

		// Token: 0x04000C78 RID: 3192
		private static KeyValuePair<string, object> truncationProperty = new KeyValuePair<string, object>(DiagnosticProperty.Trunc.ToString(), "Trunc");

		// Token: 0x04000C79 RID: 3193
		private static CsvRowBuffer csvRowBuffer = new CsvRowBuffer(0);

		// Token: 0x04000C7A RID: 3194
		private static CsvArrayDecoder decoder = new CsvArrayDecoder(typeof(KeyValuePair<string, object>));

		// Token: 0x04000C7B RID: 3195
		private DiagnosticsLevel diagnosticsLevel;

		// Token: 0x04000C7C RID: 3196
		private int requestId;

		// Token: 0x04000C7D RID: 3197
		private long startTime = DateTime.UtcNow.ToFileTime();

		// Token: 0x04000C7E RID: 3198
		private int lastRts;

		// Token: 0x04000C7F RID: 3199
		private List<ICollection<KeyValuePair<string, object>>> data = new List<ICollection<KeyValuePair<string, object>>>(30);

		// Token: 0x04000C80 RID: 3200
		private List<KeyValuePair<string, object>> currentEvent;
	}
}
