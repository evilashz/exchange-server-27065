using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x02000024 RID: 36
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ObjectNotDisposedException<T> : Exception where T : IDisposable
	{
		// Token: 0x060000BB RID: 187 RVA: 0x0000413A File Offset: 0x0000233A
		public ObjectNotDisposedException(string ctorStackTrace, bool wasReset) : this()
		{
			this.stackTrace = ctorStackTrace;
			this.wasReset = wasReset;
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00004150 File Offset: 0x00002350
		private ObjectNotDisposedException()
		{
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000BD RID: 189 RVA: 0x00004158 File Offset: 0x00002358
		public override string StackTrace
		{
			get
			{
				return this.stackTrace;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000BE RID: 190 RVA: 0x00004160 File Offset: 0x00002360
		public override string Message
		{
			get
			{
				string str = "This object implements interface IDisposable and its Dispose method was never called." + Environment.NewLine;
				return str + (this.wasReset ? "The stack trace reflects the last point where SetReportedStacktraceToCurrentLocation was called." : "The stack trace was taken at the time the object was constructed.");
			}
		}

		// Token: 0x060000BF RID: 191 RVA: 0x0000419C File Offset: 0x0000239C
		public override string ToString()
		{
			string text = base.ToString();
			if (this.stackTrace != null)
			{
				text = text + Environment.NewLine + this.stackTrace;
			}
			return text;
		}

		// Token: 0x040000B3 RID: 179
		private string stackTrace;

		// Token: 0x040000B4 RID: 180
		private bool wasReset;
	}
}
