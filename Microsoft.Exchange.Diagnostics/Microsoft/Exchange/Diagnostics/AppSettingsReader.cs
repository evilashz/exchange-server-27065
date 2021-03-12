using System;
using System.Configuration;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020001CA RID: 458
	public abstract class AppSettingsReader<T>
	{
		// Token: 0x06000CD3 RID: 3283 RVA: 0x0002F4AE File Offset: 0x0002D6AE
		public AppSettingsReader(string name, T defaultValue)
		{
			this.Name = name;
			this.value = defaultValue;
		}

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06000CD4 RID: 3284 RVA: 0x0002F4CF File Offset: 0x0002D6CF
		// (set) Token: 0x06000CD5 RID: 3285 RVA: 0x0002F4D7 File Offset: 0x0002D6D7
		public string Name { get; private set; }

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06000CD6 RID: 3286 RVA: 0x0002F4E0 File Offset: 0x0002D6E0
		public T Value
		{
			get
			{
				this.InitializeIfNeeded();
				return this.value;
			}
		}

		// Token: 0x06000CD7 RID: 3287 RVA: 0x0002F4F0 File Offset: 0x0002D6F0
		internal void Initialize(T value)
		{
			lock (this.locker)
			{
				this.initialized = true;
				this.value = value;
			}
		}

		// Token: 0x06000CD8 RID: 3288
		protected abstract bool TryParseValue(string inputValue, out T outputValue);

		// Token: 0x06000CD9 RID: 3289 RVA: 0x0002F538 File Offset: 0x0002D738
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
							if (text != null && this.TryParseValue(text, out t))
							{
								this.value = t;
							}
						}
						catch (ConfigurationErrorsException)
						{
						}
						finally
						{
							this.initialized = true;
						}
					}
				}
			}
		}

		// Token: 0x04000980 RID: 2432
		private T value;

		// Token: 0x04000981 RID: 2433
		private bool initialized;

		// Token: 0x04000982 RID: 2434
		private object locker = new object();
	}
}
