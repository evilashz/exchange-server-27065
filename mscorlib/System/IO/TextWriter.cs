using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.IO
{
	// Token: 0x020001A7 RID: 423
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public abstract class TextWriter : MarshalByRefObject, IDisposable
	{
		// Token: 0x06001A3F RID: 6719 RVA: 0x00057B1B File Offset: 0x00055D1B
		[__DynamicallyInvokable]
		protected TextWriter()
		{
			this.InternalFormatProvider = null;
		}

		// Token: 0x06001A40 RID: 6720 RVA: 0x00057B40 File Offset: 0x00055D40
		[__DynamicallyInvokable]
		protected TextWriter(IFormatProvider formatProvider)
		{
			this.InternalFormatProvider = formatProvider;
		}

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06001A41 RID: 6721 RVA: 0x00057B65 File Offset: 0x00055D65
		[__DynamicallyInvokable]
		public virtual IFormatProvider FormatProvider
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.InternalFormatProvider == null)
				{
					return Thread.CurrentThread.CurrentCulture;
				}
				return this.InternalFormatProvider;
			}
		}

		// Token: 0x06001A42 RID: 6722 RVA: 0x00057B80 File Offset: 0x00055D80
		public virtual void Close()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001A43 RID: 6723 RVA: 0x00057B8F File Offset: 0x00055D8F
		[__DynamicallyInvokable]
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x06001A44 RID: 6724 RVA: 0x00057B91 File Offset: 0x00055D91
		[__DynamicallyInvokable]
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001A45 RID: 6725 RVA: 0x00057BA0 File Offset: 0x00055DA0
		[__DynamicallyInvokable]
		public virtual void Flush()
		{
		}

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06001A46 RID: 6726
		[__DynamicallyInvokable]
		public abstract Encoding Encoding { [__DynamicallyInvokable] get; }

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06001A47 RID: 6727 RVA: 0x00057BA2 File Offset: 0x00055DA2
		// (set) Token: 0x06001A48 RID: 6728 RVA: 0x00057BAF File Offset: 0x00055DAF
		[__DynamicallyInvokable]
		public virtual string NewLine
		{
			[__DynamicallyInvokable]
			get
			{
				return new string(this.CoreNewLine);
			}
			[__DynamicallyInvokable]
			set
			{
				if (value == null)
				{
					value = "\r\n";
				}
				this.CoreNewLine = value.ToCharArray();
			}
		}

		// Token: 0x06001A49 RID: 6729 RVA: 0x00057BC7 File Offset: 0x00055DC7
		[HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
		public static TextWriter Synchronized(TextWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (writer is TextWriter.SyncTextWriter)
			{
				return writer;
			}
			return new TextWriter.SyncTextWriter(writer);
		}

		// Token: 0x06001A4A RID: 6730 RVA: 0x00057BE7 File Offset: 0x00055DE7
		[__DynamicallyInvokable]
		public virtual void Write(char value)
		{
		}

		// Token: 0x06001A4B RID: 6731 RVA: 0x00057BE9 File Offset: 0x00055DE9
		[__DynamicallyInvokable]
		public virtual void Write(char[] buffer)
		{
			if (buffer != null)
			{
				this.Write(buffer, 0, buffer.Length);
			}
		}

		// Token: 0x06001A4C RID: 6732 RVA: 0x00057BFC File Offset: 0x00055DFC
		[__DynamicallyInvokable]
		public virtual void Write(char[] buffer, int index, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer", Environment.GetResourceString("ArgumentNull_Buffer"));
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (buffer.Length - index < count)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
			}
			for (int i = 0; i < count; i++)
			{
				this.Write(buffer[index + i]);
			}
		}

		// Token: 0x06001A4D RID: 6733 RVA: 0x00057C82 File Offset: 0x00055E82
		[__DynamicallyInvokable]
		public virtual void Write(bool value)
		{
			this.Write(value ? "True" : "False");
		}

		// Token: 0x06001A4E RID: 6734 RVA: 0x00057C99 File Offset: 0x00055E99
		[__DynamicallyInvokable]
		public virtual void Write(int value)
		{
			this.Write(value.ToString(this.FormatProvider));
		}

		// Token: 0x06001A4F RID: 6735 RVA: 0x00057CAE File Offset: 0x00055EAE
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public virtual void Write(uint value)
		{
			this.Write(value.ToString(this.FormatProvider));
		}

		// Token: 0x06001A50 RID: 6736 RVA: 0x00057CC3 File Offset: 0x00055EC3
		[__DynamicallyInvokable]
		public virtual void Write(long value)
		{
			this.Write(value.ToString(this.FormatProvider));
		}

		// Token: 0x06001A51 RID: 6737 RVA: 0x00057CD8 File Offset: 0x00055ED8
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public virtual void Write(ulong value)
		{
			this.Write(value.ToString(this.FormatProvider));
		}

		// Token: 0x06001A52 RID: 6738 RVA: 0x00057CED File Offset: 0x00055EED
		[__DynamicallyInvokable]
		public virtual void Write(float value)
		{
			this.Write(value.ToString(this.FormatProvider));
		}

		// Token: 0x06001A53 RID: 6739 RVA: 0x00057D02 File Offset: 0x00055F02
		[__DynamicallyInvokable]
		public virtual void Write(double value)
		{
			this.Write(value.ToString(this.FormatProvider));
		}

		// Token: 0x06001A54 RID: 6740 RVA: 0x00057D17 File Offset: 0x00055F17
		[__DynamicallyInvokable]
		public virtual void Write(decimal value)
		{
			this.Write(value.ToString(this.FormatProvider));
		}

		// Token: 0x06001A55 RID: 6741 RVA: 0x00057D2C File Offset: 0x00055F2C
		[__DynamicallyInvokable]
		public virtual void Write(string value)
		{
			if (value != null)
			{
				this.Write(value.ToCharArray());
			}
		}

		// Token: 0x06001A56 RID: 6742 RVA: 0x00057D40 File Offset: 0x00055F40
		[__DynamicallyInvokable]
		public virtual void Write(object value)
		{
			if (value != null)
			{
				IFormattable formattable = value as IFormattable;
				if (formattable != null)
				{
					this.Write(formattable.ToString(null, this.FormatProvider));
					return;
				}
				this.Write(value.ToString());
			}
		}

		// Token: 0x06001A57 RID: 6743 RVA: 0x00057D7A File Offset: 0x00055F7A
		[__DynamicallyInvokable]
		public virtual void Write(string format, object arg0)
		{
			this.Write(string.Format(this.FormatProvider, format, arg0));
		}

		// Token: 0x06001A58 RID: 6744 RVA: 0x00057D8F File Offset: 0x00055F8F
		[__DynamicallyInvokable]
		public virtual void Write(string format, object arg0, object arg1)
		{
			this.Write(string.Format(this.FormatProvider, format, arg0, arg1));
		}

		// Token: 0x06001A59 RID: 6745 RVA: 0x00057DA5 File Offset: 0x00055FA5
		[__DynamicallyInvokable]
		public virtual void Write(string format, object arg0, object arg1, object arg2)
		{
			this.Write(string.Format(this.FormatProvider, format, arg0, arg1, arg2));
		}

		// Token: 0x06001A5A RID: 6746 RVA: 0x00057DBD File Offset: 0x00055FBD
		[__DynamicallyInvokable]
		public virtual void Write(string format, params object[] arg)
		{
			this.Write(string.Format(this.FormatProvider, format, arg));
		}

		// Token: 0x06001A5B RID: 6747 RVA: 0x00057DD2 File Offset: 0x00055FD2
		[__DynamicallyInvokable]
		public virtual void WriteLine()
		{
			this.Write(this.CoreNewLine);
		}

		// Token: 0x06001A5C RID: 6748 RVA: 0x00057DE0 File Offset: 0x00055FE0
		[__DynamicallyInvokable]
		public virtual void WriteLine(char value)
		{
			this.Write(value);
			this.WriteLine();
		}

		// Token: 0x06001A5D RID: 6749 RVA: 0x00057DEF File Offset: 0x00055FEF
		[__DynamicallyInvokable]
		public virtual void WriteLine(char[] buffer)
		{
			this.Write(buffer);
			this.WriteLine();
		}

		// Token: 0x06001A5E RID: 6750 RVA: 0x00057DFE File Offset: 0x00055FFE
		[__DynamicallyInvokable]
		public virtual void WriteLine(char[] buffer, int index, int count)
		{
			this.Write(buffer, index, count);
			this.WriteLine();
		}

		// Token: 0x06001A5F RID: 6751 RVA: 0x00057E0F File Offset: 0x0005600F
		[__DynamicallyInvokable]
		public virtual void WriteLine(bool value)
		{
			this.Write(value);
			this.WriteLine();
		}

		// Token: 0x06001A60 RID: 6752 RVA: 0x00057E1E File Offset: 0x0005601E
		[__DynamicallyInvokable]
		public virtual void WriteLine(int value)
		{
			this.Write(value);
			this.WriteLine();
		}

		// Token: 0x06001A61 RID: 6753 RVA: 0x00057E2D File Offset: 0x0005602D
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public virtual void WriteLine(uint value)
		{
			this.Write(value);
			this.WriteLine();
		}

		// Token: 0x06001A62 RID: 6754 RVA: 0x00057E3C File Offset: 0x0005603C
		[__DynamicallyInvokable]
		public virtual void WriteLine(long value)
		{
			this.Write(value);
			this.WriteLine();
		}

		// Token: 0x06001A63 RID: 6755 RVA: 0x00057E4B File Offset: 0x0005604B
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public virtual void WriteLine(ulong value)
		{
			this.Write(value);
			this.WriteLine();
		}

		// Token: 0x06001A64 RID: 6756 RVA: 0x00057E5A File Offset: 0x0005605A
		[__DynamicallyInvokable]
		public virtual void WriteLine(float value)
		{
			this.Write(value);
			this.WriteLine();
		}

		// Token: 0x06001A65 RID: 6757 RVA: 0x00057E69 File Offset: 0x00056069
		[__DynamicallyInvokable]
		public virtual void WriteLine(double value)
		{
			this.Write(value);
			this.WriteLine();
		}

		// Token: 0x06001A66 RID: 6758 RVA: 0x00057E78 File Offset: 0x00056078
		[__DynamicallyInvokable]
		public virtual void WriteLine(decimal value)
		{
			this.Write(value);
			this.WriteLine();
		}

		// Token: 0x06001A67 RID: 6759 RVA: 0x00057E88 File Offset: 0x00056088
		[__DynamicallyInvokable]
		public virtual void WriteLine(string value)
		{
			if (value == null)
			{
				this.WriteLine();
				return;
			}
			int length = value.Length;
			int num = this.CoreNewLine.Length;
			char[] array = new char[length + num];
			value.CopyTo(0, array, 0, length);
			if (num == 2)
			{
				array[length] = this.CoreNewLine[0];
				array[length + 1] = this.CoreNewLine[1];
			}
			else if (num == 1)
			{
				array[length] = this.CoreNewLine[0];
			}
			else
			{
				Buffer.InternalBlockCopy(this.CoreNewLine, 0, array, length * 2, num * 2);
			}
			this.Write(array, 0, length + num);
		}

		// Token: 0x06001A68 RID: 6760 RVA: 0x00057F10 File Offset: 0x00056110
		[__DynamicallyInvokable]
		public virtual void WriteLine(object value)
		{
			if (value == null)
			{
				this.WriteLine();
				return;
			}
			IFormattable formattable = value as IFormattable;
			if (formattable != null)
			{
				this.WriteLine(formattable.ToString(null, this.FormatProvider));
				return;
			}
			this.WriteLine(value.ToString());
		}

		// Token: 0x06001A69 RID: 6761 RVA: 0x00057F51 File Offset: 0x00056151
		[__DynamicallyInvokable]
		public virtual void WriteLine(string format, object arg0)
		{
			this.WriteLine(string.Format(this.FormatProvider, format, arg0));
		}

		// Token: 0x06001A6A RID: 6762 RVA: 0x00057F66 File Offset: 0x00056166
		[__DynamicallyInvokable]
		public virtual void WriteLine(string format, object arg0, object arg1)
		{
			this.WriteLine(string.Format(this.FormatProvider, format, arg0, arg1));
		}

		// Token: 0x06001A6B RID: 6763 RVA: 0x00057F7C File Offset: 0x0005617C
		[__DynamicallyInvokable]
		public virtual void WriteLine(string format, object arg0, object arg1, object arg2)
		{
			this.WriteLine(string.Format(this.FormatProvider, format, arg0, arg1, arg2));
		}

		// Token: 0x06001A6C RID: 6764 RVA: 0x00057F94 File Offset: 0x00056194
		[__DynamicallyInvokable]
		public virtual void WriteLine(string format, params object[] arg)
		{
			this.WriteLine(string.Format(this.FormatProvider, format, arg));
		}

		// Token: 0x06001A6D RID: 6765 RVA: 0x00057FAC File Offset: 0x000561AC
		[ComVisible(false)]
		[__DynamicallyInvokable]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual Task WriteAsync(char value)
		{
			Tuple<TextWriter, char> state = new Tuple<TextWriter, char>(this, value);
			return Task.Factory.StartNew(TextWriter._WriteCharDelegate, state, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
		}

		// Token: 0x06001A6E RID: 6766 RVA: 0x00057FDC File Offset: 0x000561DC
		[ComVisible(false)]
		[__DynamicallyInvokable]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual Task WriteAsync(string value)
		{
			Tuple<TextWriter, string> state = new Tuple<TextWriter, string>(this, value);
			return Task.Factory.StartNew(TextWriter._WriteStringDelegate, state, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
		}

		// Token: 0x06001A6F RID: 6767 RVA: 0x0005800C File Offset: 0x0005620C
		[ComVisible(false)]
		[__DynamicallyInvokable]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task WriteAsync(char[] buffer)
		{
			if (buffer == null)
			{
				return Task.CompletedTask;
			}
			return this.WriteAsync(buffer, 0, buffer.Length);
		}

		// Token: 0x06001A70 RID: 6768 RVA: 0x00058024 File Offset: 0x00056224
		[ComVisible(false)]
		[__DynamicallyInvokable]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual Task WriteAsync(char[] buffer, int index, int count)
		{
			Tuple<TextWriter, char[], int, int> state = new Tuple<TextWriter, char[], int, int>(this, buffer, index, count);
			return Task.Factory.StartNew(TextWriter._WriteCharArrayRangeDelegate, state, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
		}

		// Token: 0x06001A71 RID: 6769 RVA: 0x00058058 File Offset: 0x00056258
		[ComVisible(false)]
		[__DynamicallyInvokable]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual Task WriteLineAsync(char value)
		{
			Tuple<TextWriter, char> state = new Tuple<TextWriter, char>(this, value);
			return Task.Factory.StartNew(TextWriter._WriteLineCharDelegate, state, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
		}

		// Token: 0x06001A72 RID: 6770 RVA: 0x00058088 File Offset: 0x00056288
		[ComVisible(false)]
		[__DynamicallyInvokable]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual Task WriteLineAsync(string value)
		{
			Tuple<TextWriter, string> state = new Tuple<TextWriter, string>(this, value);
			return Task.Factory.StartNew(TextWriter._WriteLineStringDelegate, state, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
		}

		// Token: 0x06001A73 RID: 6771 RVA: 0x000580B8 File Offset: 0x000562B8
		[ComVisible(false)]
		[__DynamicallyInvokable]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task WriteLineAsync(char[] buffer)
		{
			if (buffer == null)
			{
				return Task.CompletedTask;
			}
			return this.WriteLineAsync(buffer, 0, buffer.Length);
		}

		// Token: 0x06001A74 RID: 6772 RVA: 0x000580D0 File Offset: 0x000562D0
		[ComVisible(false)]
		[__DynamicallyInvokable]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual Task WriteLineAsync(char[] buffer, int index, int count)
		{
			Tuple<TextWriter, char[], int, int> state = new Tuple<TextWriter, char[], int, int>(this, buffer, index, count);
			return Task.Factory.StartNew(TextWriter._WriteLineCharArrayRangeDelegate, state, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
		}

		// Token: 0x06001A75 RID: 6773 RVA: 0x00058102 File Offset: 0x00056302
		[ComVisible(false)]
		[__DynamicallyInvokable]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual Task WriteLineAsync()
		{
			return this.WriteAsync(this.CoreNewLine);
		}

		// Token: 0x06001A76 RID: 6774 RVA: 0x00058110 File Offset: 0x00056310
		[ComVisible(false)]
		[__DynamicallyInvokable]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual Task FlushAsync()
		{
			return Task.Factory.StartNew(TextWriter._FlushDelegate, this, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
		}

		// Token: 0x04000925 RID: 2341
		[__DynamicallyInvokable]
		public static readonly TextWriter Null = new TextWriter.NullTextWriter();

		// Token: 0x04000926 RID: 2342
		[NonSerialized]
		private static Action<object> _WriteCharDelegate = delegate(object state)
		{
			Tuple<TextWriter, char> tuple = (Tuple<TextWriter, char>)state;
			tuple.Item1.Write(tuple.Item2);
		};

		// Token: 0x04000927 RID: 2343
		[NonSerialized]
		private static Action<object> _WriteStringDelegate = delegate(object state)
		{
			Tuple<TextWriter, string> tuple = (Tuple<TextWriter, string>)state;
			tuple.Item1.Write(tuple.Item2);
		};

		// Token: 0x04000928 RID: 2344
		[NonSerialized]
		private static Action<object> _WriteCharArrayRangeDelegate = delegate(object state)
		{
			Tuple<TextWriter, char[], int, int> tuple = (Tuple<TextWriter, char[], int, int>)state;
			tuple.Item1.Write(tuple.Item2, tuple.Item3, tuple.Item4);
		};

		// Token: 0x04000929 RID: 2345
		[NonSerialized]
		private static Action<object> _WriteLineCharDelegate = delegate(object state)
		{
			Tuple<TextWriter, char> tuple = (Tuple<TextWriter, char>)state;
			tuple.Item1.WriteLine(tuple.Item2);
		};

		// Token: 0x0400092A RID: 2346
		[NonSerialized]
		private static Action<object> _WriteLineStringDelegate = delegate(object state)
		{
			Tuple<TextWriter, string> tuple = (Tuple<TextWriter, string>)state;
			tuple.Item1.WriteLine(tuple.Item2);
		};

		// Token: 0x0400092B RID: 2347
		[NonSerialized]
		private static Action<object> _WriteLineCharArrayRangeDelegate = delegate(object state)
		{
			Tuple<TextWriter, char[], int, int> tuple = (Tuple<TextWriter, char[], int, int>)state;
			tuple.Item1.WriteLine(tuple.Item2, tuple.Item3, tuple.Item4);
		};

		// Token: 0x0400092C RID: 2348
		[NonSerialized]
		private static Action<object> _FlushDelegate = delegate(object state)
		{
			((TextWriter)state).Flush();
		};

		// Token: 0x0400092D RID: 2349
		private const string InitialNewLine = "\r\n";

		// Token: 0x0400092E RID: 2350
		[__DynamicallyInvokable]
		protected char[] CoreNewLine = new char[]
		{
			'\r',
			'\n'
		};

		// Token: 0x0400092F RID: 2351
		private IFormatProvider InternalFormatProvider;

		// Token: 0x02000AFA RID: 2810
		[Serializable]
		private sealed class NullTextWriter : TextWriter
		{
			// Token: 0x060069FD RID: 27133 RVA: 0x0016E80D File Offset: 0x0016CA0D
			internal NullTextWriter() : base(CultureInfo.InvariantCulture)
			{
			}

			// Token: 0x17001203 RID: 4611
			// (get) Token: 0x060069FE RID: 27134 RVA: 0x0016E81A File Offset: 0x0016CA1A
			public override Encoding Encoding
			{
				get
				{
					return Encoding.Default;
				}
			}

			// Token: 0x060069FF RID: 27135 RVA: 0x0016E821 File Offset: 0x0016CA21
			public override void Write(char[] buffer, int index, int count)
			{
			}

			// Token: 0x06006A00 RID: 27136 RVA: 0x0016E823 File Offset: 0x0016CA23
			public override void Write(string value)
			{
			}

			// Token: 0x06006A01 RID: 27137 RVA: 0x0016E825 File Offset: 0x0016CA25
			public override void WriteLine()
			{
			}

			// Token: 0x06006A02 RID: 27138 RVA: 0x0016E827 File Offset: 0x0016CA27
			public override void WriteLine(string value)
			{
			}

			// Token: 0x06006A03 RID: 27139 RVA: 0x0016E829 File Offset: 0x0016CA29
			public override void WriteLine(object value)
			{
			}
		}

		// Token: 0x02000AFB RID: 2811
		[Serializable]
		internal sealed class SyncTextWriter : TextWriter, IDisposable
		{
			// Token: 0x06006A04 RID: 27140 RVA: 0x0016E82B File Offset: 0x0016CA2B
			internal SyncTextWriter(TextWriter t) : base(t.FormatProvider)
			{
				this._out = t;
			}

			// Token: 0x17001204 RID: 4612
			// (get) Token: 0x06006A05 RID: 27141 RVA: 0x0016E840 File Offset: 0x0016CA40
			public override Encoding Encoding
			{
				get
				{
					return this._out.Encoding;
				}
			}

			// Token: 0x17001205 RID: 4613
			// (get) Token: 0x06006A06 RID: 27142 RVA: 0x0016E84D File Offset: 0x0016CA4D
			public override IFormatProvider FormatProvider
			{
				get
				{
					return this._out.FormatProvider;
				}
			}

			// Token: 0x17001206 RID: 4614
			// (get) Token: 0x06006A07 RID: 27143 RVA: 0x0016E85A File Offset: 0x0016CA5A
			// (set) Token: 0x06006A08 RID: 27144 RVA: 0x0016E867 File Offset: 0x0016CA67
			public override string NewLine
			{
				[MethodImpl(MethodImplOptions.Synchronized)]
				get
				{
					return this._out.NewLine;
				}
				[MethodImpl(MethodImplOptions.Synchronized)]
				set
				{
					this._out.NewLine = value;
				}
			}

			// Token: 0x06006A09 RID: 27145 RVA: 0x0016E875 File Offset: 0x0016CA75
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Close()
			{
				this._out.Close();
			}

			// Token: 0x06006A0A RID: 27146 RVA: 0x0016E882 File Offset: 0x0016CA82
			[MethodImpl(MethodImplOptions.Synchronized)]
			protected override void Dispose(bool disposing)
			{
				if (disposing)
				{
					((IDisposable)this._out).Dispose();
				}
			}

			// Token: 0x06006A0B RID: 27147 RVA: 0x0016E892 File Offset: 0x0016CA92
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Flush()
			{
				this._out.Flush();
			}

			// Token: 0x06006A0C RID: 27148 RVA: 0x0016E89F File Offset: 0x0016CA9F
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(char value)
			{
				this._out.Write(value);
			}

			// Token: 0x06006A0D RID: 27149 RVA: 0x0016E8AD File Offset: 0x0016CAAD
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(char[] buffer)
			{
				this._out.Write(buffer);
			}

			// Token: 0x06006A0E RID: 27150 RVA: 0x0016E8BB File Offset: 0x0016CABB
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(char[] buffer, int index, int count)
			{
				this._out.Write(buffer, index, count);
			}

			// Token: 0x06006A0F RID: 27151 RVA: 0x0016E8CB File Offset: 0x0016CACB
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(bool value)
			{
				this._out.Write(value);
			}

			// Token: 0x06006A10 RID: 27152 RVA: 0x0016E8D9 File Offset: 0x0016CAD9
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(int value)
			{
				this._out.Write(value);
			}

			// Token: 0x06006A11 RID: 27153 RVA: 0x0016E8E7 File Offset: 0x0016CAE7
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(uint value)
			{
				this._out.Write(value);
			}

			// Token: 0x06006A12 RID: 27154 RVA: 0x0016E8F5 File Offset: 0x0016CAF5
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(long value)
			{
				this._out.Write(value);
			}

			// Token: 0x06006A13 RID: 27155 RVA: 0x0016E903 File Offset: 0x0016CB03
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(ulong value)
			{
				this._out.Write(value);
			}

			// Token: 0x06006A14 RID: 27156 RVA: 0x0016E911 File Offset: 0x0016CB11
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(float value)
			{
				this._out.Write(value);
			}

			// Token: 0x06006A15 RID: 27157 RVA: 0x0016E91F File Offset: 0x0016CB1F
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(double value)
			{
				this._out.Write(value);
			}

			// Token: 0x06006A16 RID: 27158 RVA: 0x0016E92D File Offset: 0x0016CB2D
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(decimal value)
			{
				this._out.Write(value);
			}

			// Token: 0x06006A17 RID: 27159 RVA: 0x0016E93B File Offset: 0x0016CB3B
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(string value)
			{
				this._out.Write(value);
			}

			// Token: 0x06006A18 RID: 27160 RVA: 0x0016E949 File Offset: 0x0016CB49
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(object value)
			{
				this._out.Write(value);
			}

			// Token: 0x06006A19 RID: 27161 RVA: 0x0016E957 File Offset: 0x0016CB57
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(string format, object arg0)
			{
				this._out.Write(format, arg0);
			}

			// Token: 0x06006A1A RID: 27162 RVA: 0x0016E966 File Offset: 0x0016CB66
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(string format, object arg0, object arg1)
			{
				this._out.Write(format, arg0, arg1);
			}

			// Token: 0x06006A1B RID: 27163 RVA: 0x0016E976 File Offset: 0x0016CB76
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(string format, object arg0, object arg1, object arg2)
			{
				this._out.Write(format, arg0, arg1, arg2);
			}

			// Token: 0x06006A1C RID: 27164 RVA: 0x0016E988 File Offset: 0x0016CB88
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(string format, params object[] arg)
			{
				this._out.Write(format, arg);
			}

			// Token: 0x06006A1D RID: 27165 RVA: 0x0016E997 File Offset: 0x0016CB97
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine()
			{
				this._out.WriteLine();
			}

			// Token: 0x06006A1E RID: 27166 RVA: 0x0016E9A4 File Offset: 0x0016CBA4
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(char value)
			{
				this._out.WriteLine(value);
			}

			// Token: 0x06006A1F RID: 27167 RVA: 0x0016E9B2 File Offset: 0x0016CBB2
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(decimal value)
			{
				this._out.WriteLine(value);
			}

			// Token: 0x06006A20 RID: 27168 RVA: 0x0016E9C0 File Offset: 0x0016CBC0
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(char[] buffer)
			{
				this._out.WriteLine(buffer);
			}

			// Token: 0x06006A21 RID: 27169 RVA: 0x0016E9CE File Offset: 0x0016CBCE
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(char[] buffer, int index, int count)
			{
				this._out.WriteLine(buffer, index, count);
			}

			// Token: 0x06006A22 RID: 27170 RVA: 0x0016E9DE File Offset: 0x0016CBDE
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(bool value)
			{
				this._out.WriteLine(value);
			}

			// Token: 0x06006A23 RID: 27171 RVA: 0x0016E9EC File Offset: 0x0016CBEC
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(int value)
			{
				this._out.WriteLine(value);
			}

			// Token: 0x06006A24 RID: 27172 RVA: 0x0016E9FA File Offset: 0x0016CBFA
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(uint value)
			{
				this._out.WriteLine(value);
			}

			// Token: 0x06006A25 RID: 27173 RVA: 0x0016EA08 File Offset: 0x0016CC08
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(long value)
			{
				this._out.WriteLine(value);
			}

			// Token: 0x06006A26 RID: 27174 RVA: 0x0016EA16 File Offset: 0x0016CC16
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(ulong value)
			{
				this._out.WriteLine(value);
			}

			// Token: 0x06006A27 RID: 27175 RVA: 0x0016EA24 File Offset: 0x0016CC24
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(float value)
			{
				this._out.WriteLine(value);
			}

			// Token: 0x06006A28 RID: 27176 RVA: 0x0016EA32 File Offset: 0x0016CC32
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(double value)
			{
				this._out.WriteLine(value);
			}

			// Token: 0x06006A29 RID: 27177 RVA: 0x0016EA40 File Offset: 0x0016CC40
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(string value)
			{
				this._out.WriteLine(value);
			}

			// Token: 0x06006A2A RID: 27178 RVA: 0x0016EA4E File Offset: 0x0016CC4E
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(object value)
			{
				this._out.WriteLine(value);
			}

			// Token: 0x06006A2B RID: 27179 RVA: 0x0016EA5C File Offset: 0x0016CC5C
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(string format, object arg0)
			{
				this._out.WriteLine(format, arg0);
			}

			// Token: 0x06006A2C RID: 27180 RVA: 0x0016EA6B File Offset: 0x0016CC6B
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(string format, object arg0, object arg1)
			{
				this._out.WriteLine(format, arg0, arg1);
			}

			// Token: 0x06006A2D RID: 27181 RVA: 0x0016EA7B File Offset: 0x0016CC7B
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(string format, object arg0, object arg1, object arg2)
			{
				this._out.WriteLine(format, arg0, arg1, arg2);
			}

			// Token: 0x06006A2E RID: 27182 RVA: 0x0016EA8D File Offset: 0x0016CC8D
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(string format, params object[] arg)
			{
				this._out.WriteLine(format, arg);
			}

			// Token: 0x06006A2F RID: 27183 RVA: 0x0016EA9C File Offset: 0x0016CC9C
			[ComVisible(false)]
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override Task WriteAsync(char value)
			{
				this.Write(value);
				return Task.CompletedTask;
			}

			// Token: 0x06006A30 RID: 27184 RVA: 0x0016EAAA File Offset: 0x0016CCAA
			[ComVisible(false)]
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override Task WriteAsync(string value)
			{
				this.Write(value);
				return Task.CompletedTask;
			}

			// Token: 0x06006A31 RID: 27185 RVA: 0x0016EAB8 File Offset: 0x0016CCB8
			[ComVisible(false)]
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override Task WriteAsync(char[] buffer, int index, int count)
			{
				this.Write(buffer, index, count);
				return Task.CompletedTask;
			}

			// Token: 0x06006A32 RID: 27186 RVA: 0x0016EAC8 File Offset: 0x0016CCC8
			[ComVisible(false)]
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override Task WriteLineAsync(char value)
			{
				this.WriteLine(value);
				return Task.CompletedTask;
			}

			// Token: 0x06006A33 RID: 27187 RVA: 0x0016EAD6 File Offset: 0x0016CCD6
			[ComVisible(false)]
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override Task WriteLineAsync(string value)
			{
				this.WriteLine(value);
				return Task.CompletedTask;
			}

			// Token: 0x06006A34 RID: 27188 RVA: 0x0016EAE4 File Offset: 0x0016CCE4
			[ComVisible(false)]
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override Task WriteLineAsync(char[] buffer, int index, int count)
			{
				this.WriteLine(buffer, index, count);
				return Task.CompletedTask;
			}

			// Token: 0x06006A35 RID: 27189 RVA: 0x0016EAF4 File Offset: 0x0016CCF4
			[ComVisible(false)]
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override Task FlushAsync()
			{
				this.Flush();
				return Task.CompletedTask;
			}

			// Token: 0x0400326C RID: 12908
			private TextWriter _out;
		}
	}
}
