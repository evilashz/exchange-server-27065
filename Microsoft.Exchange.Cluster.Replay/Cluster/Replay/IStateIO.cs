using System;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Common.Cluster;
using Microsoft.Exchange.Common.HA;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000116 RID: 278
	internal abstract class IStateIO
	{
		// Token: 0x06000A80 RID: 2688 RVA: 0x0002F7AE File Offset: 0x0002D9AE
		public void UseSetBrokenForIOFailures(ISetBroken setBroken)
		{
			this.m_setBroken = setBroken;
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x06000A81 RID: 2689 RVA: 0x0002F7B7 File Offset: 0x0002D9B7
		public bool IOFailures
		{
			get
			{
				return this.m_fSetBrokenCalled;
			}
		}

		// Token: 0x06000A82 RID: 2690 RVA: 0x0002F7C0 File Offset: 0x0002D9C0
		protected void IOFailure(Exception ex)
		{
			bool flag = false;
			lock (this)
			{
				if (!this.UseIOFailure)
				{
					throw ex;
				}
				flag = !this.m_fSetBrokenCalled;
				this.m_fSetBrokenCalled = true;
			}
			if (flag)
			{
				this.m_setBroken.SetBroken(FailureTag.NoOp, ReplayEventLogConstants.Tuple_SavedStateError, ex, new string[]
				{
					ex.ToString()
				});
			}
		}

		// Token: 0x06000A83 RID: 2691 RVA: 0x0002F83C File Offset: 0x0002DA3C
		public void IOFailureTestHook(Exception e)
		{
			this.IOFailure(e);
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06000A84 RID: 2692 RVA: 0x0002F845 File Offset: 0x0002DA45
		protected bool UseIOFailure
		{
			get
			{
				return this.m_setBroken != null;
			}
		}

		// Token: 0x06000A85 RID: 2693 RVA: 0x0002F854 File Offset: 0x0002DA54
		public bool TryReadLong(string valueName, long defaultValue, out long value)
		{
			string text;
			bool result = this.TryReadFromStore(valueName, defaultValue.ToString(), out text);
			IStateIO.trace.TraceDebug<string, string>(0L, "ReadLong: {0}={1}", valueName, text);
			try
			{
				value = long.Parse(text);
			}
			catch (FormatException innerException)
			{
				this.IOFailure(new InvalidSavedStateException(innerException));
				value = defaultValue;
				return false;
			}
			catch (OverflowException innerException2)
			{
				this.IOFailure(new InvalidSavedStateException(innerException2));
				value = defaultValue;
				return false;
			}
			return result;
		}

		// Token: 0x06000A86 RID: 2694 RVA: 0x0002F8DC File Offset: 0x0002DADC
		public bool TryReadBool(string valueName, bool defaultValue, out bool value)
		{
			string text;
			bool result = this.TryReadFromStore(valueName, defaultValue.ToString(), out text);
			IStateIO.trace.TraceDebug<string, string>((long)this.GetHashCode(), "ReadBool: {0}={1}", valueName, text);
			value = Cluster.StringIEquals(text, "true");
			return result;
		}

		// Token: 0x06000A87 RID: 2695 RVA: 0x0002F920 File Offset: 0x0002DB20
		public bool TryReadString(string valueName, string defaultValue, out string value)
		{
			bool result = this.TryReadFromStore(valueName, defaultValue, out value);
			IStateIO.trace.TraceDebug<string, string>((long)this.GetHashCode(), "ReadStr: {0}={1}", valueName, value);
			if (string.IsNullOrEmpty(value))
			{
				value = null;
			}
			return result;
		}

		// Token: 0x06000A88 RID: 2696 RVA: 0x0002F960 File Offset: 0x0002DB60
		public bool TryReadDateTime(string valueName, DateTime defaultValue, out DateTime value)
		{
			long fileTime;
			bool result = this.TryReadLong(valueName, defaultValue.ToFileTime(), out fileTime);
			value = DateTime.FromFileTimeUtc(fileTime);
			return result;
		}

		// Token: 0x06000A89 RID: 2697 RVA: 0x0002F98C File Offset: 0x0002DB8C
		public bool TryReadEnum<T>(string valueName, T defaultValue, out T value)
		{
			string text;
			bool result = this.TryReadFromStore(valueName, defaultValue.ToString(), out text);
			IStateIO.trace.TraceDebug<string, string>((long)this.GetHashCode(), "ReadEnum: {0}={1}", valueName, text);
			try
			{
				value = (T)((object)Enum.Parse(typeof(T), text));
			}
			catch (ArgumentException innerException)
			{
				this.IOFailure(new InvalidSavedStateException(innerException));
				value = defaultValue;
				return false;
			}
			return result;
		}

		// Token: 0x06000A8A RID: 2698
		protected abstract bool TryReadFromStore(string valueName, string defaultValue, out string value);

		// Token: 0x06000A8B RID: 2699 RVA: 0x0002FA10 File Offset: 0x0002DC10
		public bool TryReadFromStoreTestHook(string valueName, string defaultValue, out string value)
		{
			return this.TryReadFromStore(valueName, defaultValue, out value);
		}

		// Token: 0x06000A8C RID: 2700 RVA: 0x0002FA1B File Offset: 0x0002DC1B
		public bool TryReadValueNames(out string[] valueNames)
		{
			return this.TryReadValueNamesInternal(out valueNames);
		}

		// Token: 0x06000A8D RID: 2701 RVA: 0x0002FA24 File Offset: 0x0002DC24
		public void WriteString(string valueName, string value, bool fCritical)
		{
			IStateIO.trace.TraceDebug<string, string>((long)this.GetHashCode(), "WriteStr: {0}={1}", valueName, value);
			this.TryWriteToStore(valueName, value ?? string.Empty, fCritical);
		}

		// Token: 0x06000A8E RID: 2702 RVA: 0x0002FA51 File Offset: 0x0002DC51
		public void WriteLong(string valueName, long value, bool fCritical)
		{
			IStateIO.trace.TraceDebug<string, long>((long)this.GetHashCode(), "WriteLong: {0}={1}", valueName, value);
			this.TryWriteToStore(valueName, value.ToString(), fCritical);
		}

		// Token: 0x06000A8F RID: 2703 RVA: 0x0002FA7B File Offset: 0x0002DC7B
		public void WriteBool(string valueName, bool value, bool fCritical)
		{
			IStateIO.trace.TraceDebug<string, bool>((long)this.GetHashCode(), "WriteBool: {0}={1}", valueName, value);
			this.TryWriteToStore(valueName, value.ToString(), fCritical);
		}

		// Token: 0x06000A90 RID: 2704 RVA: 0x0002FAA5 File Offset: 0x0002DCA5
		public void WriteDateTime(string valueName, DateTime value, bool fCritical)
		{
			IStateIO.trace.TraceDebug<string, DateTime>((long)this.GetHashCode(), "WriteDT: {0}={1}", valueName, value);
			this.WriteLong(valueName, value.ToFileTime(), fCritical);
		}

		// Token: 0x06000A91 RID: 2705 RVA: 0x0002FACE File Offset: 0x0002DCCE
		public void WriteEnum<T>(string valueName, T value, bool fCritical)
		{
			IStateIO.trace.TraceDebug<string, T>((long)this.GetHashCode(), "WriteEnum: {0}={1}", valueName, value);
			this.TryWriteToStore(valueName, value.ToString(), fCritical);
		}

		// Token: 0x06000A92 RID: 2706
		protected abstract bool TryWriteToStore(string valueName, string value, bool fCritical);

		// Token: 0x06000A93 RID: 2707
		protected abstract bool TryReadValueNamesInternal(out string[] valueNames);

		// Token: 0x06000A94 RID: 2708 RVA: 0x0002FAFE File Offset: 0x0002DCFE
		public bool TryWriteToStoreTestHook(string valueName, string value, bool fCritical)
		{
			return this.TryWriteToStore(valueName, value, fCritical);
		}

		// Token: 0x06000A95 RID: 2709
		public abstract void DeleteState();

		// Token: 0x04000474 RID: 1140
		private ISetBroken m_setBroken;

		// Token: 0x04000475 RID: 1141
		private bool m_fSetBrokenCalled;

		// Token: 0x04000476 RID: 1142
		private static Trace trace = ExTraceGlobals.StateTracer;
	}
}
