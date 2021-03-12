using System;
using System.IO;
using System.Text;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.MailboxRules
{
	// Token: 0x02000C08 RID: 3080
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class TraceFormatter : DisposableObject
	{
		// Token: 0x06006DEC RID: 28140 RVA: 0x001D82C1 File Offset: 0x001D64C1
		public TraceFormatter(bool saveTrace)
		{
			if (saveTrace)
			{
				this.traceStream = TemporaryStorage.Create();
				this.traceWriter = new StreamWriter(this.traceStream);
			}
		}

		// Token: 0x17001DD0 RID: 7632
		// (get) Token: 0x06006DED RID: 28141 RVA: 0x001D82E8 File Offset: 0x001D64E8
		// (set) Token: 0x06006DEE RID: 28142 RVA: 0x001D82F7 File Offset: 0x001D64F7
		public int NestedLevel
		{
			get
			{
				this.CheckDisposed(null);
				return this.nestedLevel;
			}
			set
			{
				this.CheckDisposed(null);
				this.nestedLevel = value;
			}
		}

		// Token: 0x17001DD1 RID: 7633
		// (get) Token: 0x06006DEF RID: 28143 RVA: 0x001D8307 File Offset: 0x001D6507
		public bool HasTraceHistory
		{
			get
			{
				this.CheckDisposed(null);
				return this.traceWriter != null;
			}
		}

		// Token: 0x06006DF0 RID: 28144 RVA: 0x001D831C File Offset: 0x001D651C
		public string Format(string message)
		{
			this.CheckDisposed(null);
			return this.InternalFormat(message, new object[0]);
		}

		// Token: 0x06006DF1 RID: 28145 RVA: 0x001D8334 File Offset: 0x001D6534
		public string Format(string format, object argument1)
		{
			this.CheckDisposed(null);
			return this.InternalFormat(format, new object[]
			{
				TraceFormatter.GetTraceValue(argument1)
			});
		}

		// Token: 0x06006DF2 RID: 28146 RVA: 0x001D8360 File Offset: 0x001D6560
		public string Format(string format, object argument1, object argument2)
		{
			this.CheckDisposed(null);
			return this.InternalFormat(format, new object[]
			{
				TraceFormatter.GetTraceValue(argument1),
				TraceFormatter.GetTraceValue(argument2)
			});
		}

		// Token: 0x06006DF3 RID: 28147 RVA: 0x001D8398 File Offset: 0x001D6598
		public string Format(string format, object argument1, object argument2, object argument3)
		{
			this.CheckDisposed(null);
			return this.InternalFormat(format, new object[]
			{
				TraceFormatter.GetTraceValue(argument1),
				TraceFormatter.GetTraceValue(argument2),
				TraceFormatter.GetTraceValue(argument3)
			});
		}

		// Token: 0x06006DF4 RID: 28148 RVA: 0x001D83D8 File Offset: 0x001D65D8
		public string Format(string format, object argument1, object argument2, object argument3, object argument4)
		{
			this.CheckDisposed(null);
			return this.InternalFormat(format, new object[]
			{
				TraceFormatter.GetTraceValue(argument1),
				TraceFormatter.GetTraceValue(argument2),
				TraceFormatter.GetTraceValue(argument3),
				TraceFormatter.GetTraceValue(argument4)
			});
		}

		// Token: 0x06006DF5 RID: 28149 RVA: 0x001D8424 File Offset: 0x001D6624
		public void CopyDataTo(Stream targetStream)
		{
			this.CheckDisposed(null);
			if (this.traceWriter == null)
			{
				return;
			}
			this.traceWriter.Flush();
			long position = this.traceStream.Position;
			this.traceStream.Position = 0L;
			byte[] array = new byte[2048];
			for (;;)
			{
				int num = this.traceStream.Read(array, 0, array.Length);
				if (num == 0)
				{
					break;
				}
				targetStream.Write(array, 0, num);
			}
			this.traceStream.Position = position;
		}

		// Token: 0x06006DF6 RID: 28150 RVA: 0x001D849B File Offset: 0x001D669B
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				Util.DisposeIfPresent(this.traceWriter);
				Util.DisposeIfPresent(this.traceStream);
			}
			base.InternalDispose(disposing);
		}

		// Token: 0x06006DF7 RID: 28151 RVA: 0x001D84BD File Offset: 0x001D66BD
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<TraceFormatter>(this);
		}

		// Token: 0x06006DF8 RID: 28152 RVA: 0x001D84C8 File Offset: 0x001D66C8
		private static string[] GetIndentationArray()
		{
			string[] array = new string[20];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new string(' ', i * 2);
			}
			return array;
		}

		// Token: 0x06006DF9 RID: 28153 RVA: 0x001D84FC File Offset: 0x001D66FC
		private static object GetTraceValue(object value)
		{
			if (value == null)
			{
				return null;
			}
			if (value.GetType() == typeof(string))
			{
				string text = (string)value;
				if (text.Length > 256)
				{
					StringBuilder stringBuilder = new StringBuilder(text, 0, 256, 259);
					stringBuilder.Append("...");
					return stringBuilder.ToString();
				}
			}
			else if (value.GetType() == typeof(byte[]))
			{
				byte[] array = (byte[])value;
				int num = Math.Min(array.Length, 256);
				StringBuilder stringBuilder2 = new StringBuilder(num * 2 + 2);
				stringBuilder2.Append('[');
				stringBuilder2.Append(HexConverter.ByteArrayToHexString(array, 0, num));
				stringBuilder2.Append(']');
				return stringBuilder2.ToString();
			}
			return value;
		}

		// Token: 0x06006DFA RID: 28154 RVA: 0x001D85C8 File Offset: 0x001D67C8
		private string InternalFormat(string format, params object[] arguments)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (this.nestedLevel != 0)
			{
				int num = Math.Min(this.nestedLevel, TraceFormatter.indentationArray.Length - 1);
				stringBuilder.Append(TraceFormatter.indentationArray[num]);
			}
			stringBuilder.AppendFormat(format, arguments);
			string text = stringBuilder.ToString();
			if (this.traceWriter != null)
			{
				this.traceWriter.WriteLine(text);
			}
			return text;
		}

		// Token: 0x04003EB0 RID: 16048
		private const int TraceStringLimit = 256;

		// Token: 0x04003EB1 RID: 16049
		private static readonly string[] indentationArray = TraceFormatter.GetIndentationArray();

		// Token: 0x04003EB2 RID: 16050
		private readonly StreamWriter traceWriter;

		// Token: 0x04003EB3 RID: 16051
		private readonly Stream traceStream;

		// Token: 0x04003EB4 RID: 16052
		private int nestedLevel;
	}
}
