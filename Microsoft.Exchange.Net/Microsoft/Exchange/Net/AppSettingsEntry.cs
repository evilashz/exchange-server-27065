using System;
using System.Configuration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000002 RID: 2
	internal abstract class AppSettingsEntry<T>
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public AppSettingsEntry(string name, T defaultValue, Trace tracer)
		{
			this.name = name;
			this.value = defaultValue;
			this.tracer = tracer;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x000020F8 File Offset: 0x000002F8
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x00002100 File Offset: 0x00000300
		public T Value
		{
			get
			{
				this.InitializeIfNeeded();
				return this.value;
			}
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002110 File Offset: 0x00000310
		private void InitializeIfNeeded()
		{
			if (!this.initialized)
			{
				lock (this.locker)
				{
					if (!this.initialized)
					{
						try
						{
							string text = ConfigurationManager.AppSettings[this.Name];
							T t;
							if (text == null)
							{
								this.Trace("Property not defined, using default");
							}
							else if (this.TryParseValue(text, out t))
							{
								this.value = t;
								this.Trace("Property defined");
							}
							else
							{
								this.Trace("Could not read valid value, using default");
							}
						}
						catch (ConfigurationErrorsException e)
						{
							this.Trace("Caught configuration exception.  Using default.", e);
						}
						finally
						{
							this.initialized = true;
						}
					}
				}
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000021E0 File Offset: 0x000003E0
		internal void Initialize(T value)
		{
			lock (this.locker)
			{
				this.initialized = true;
				this.value = value;
			}
			this.Trace("Explictly set");
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002234 File Offset: 0x00000434
		private void Trace(string message)
		{
			if (this.tracer != null)
			{
				this.tracer.TraceDebug<string, string, T>((long)this.GetHashCode(), "{0}: {1}={2}", message, this.name, this.value);
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002264 File Offset: 0x00000464
		private void Trace(string message, Exception e)
		{
			if (this.tracer != null)
			{
				this.tracer.TraceError((long)this.GetHashCode(), "{0}: {1}={2}. Exception: {3}", new object[]
				{
					message,
					this.name,
					this.value,
					e
				});
			}
		}

		// Token: 0x06000008 RID: 8
		protected abstract bool TryParseValue(string inputValue, out T outputValue);

		// Token: 0x04000001 RID: 1
		private string name;

		// Token: 0x04000002 RID: 2
		private T value;

		// Token: 0x04000003 RID: 3
		private Trace tracer;

		// Token: 0x04000004 RID: 4
		private bool initialized;

		// Token: 0x04000005 RID: 5
		private object locker = new object();
	}
}
