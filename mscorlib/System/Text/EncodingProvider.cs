using System;
using System.Runtime.InteropServices;

namespace System.Text
{
	// Token: 0x02000A49 RID: 2633
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public abstract class EncodingProvider
	{
		// Token: 0x06006772 RID: 26482 RVA: 0x0015C6A2 File Offset: 0x0015A8A2
		[__DynamicallyInvokable]
		public EncodingProvider()
		{
		}

		// Token: 0x06006773 RID: 26483
		[__DynamicallyInvokable]
		public abstract Encoding GetEncoding(string name);

		// Token: 0x06006774 RID: 26484
		[__DynamicallyInvokable]
		public abstract Encoding GetEncoding(int codepage);

		// Token: 0x06006775 RID: 26485 RVA: 0x0015C6AC File Offset: 0x0015A8AC
		[__DynamicallyInvokable]
		public virtual Encoding GetEncoding(string name, EncoderFallback encoderFallback, DecoderFallback decoderFallback)
		{
			Encoding encoding = this.GetEncoding(name);
			if (encoding != null)
			{
				encoding = (Encoding)this.GetEncoding(name).Clone();
				encoding.EncoderFallback = encoderFallback;
				encoding.DecoderFallback = decoderFallback;
			}
			return encoding;
		}

		// Token: 0x06006776 RID: 26486 RVA: 0x0015C6E8 File Offset: 0x0015A8E8
		[__DynamicallyInvokable]
		public virtual Encoding GetEncoding(int codepage, EncoderFallback encoderFallback, DecoderFallback decoderFallback)
		{
			Encoding encoding = this.GetEncoding(codepage);
			if (encoding != null)
			{
				encoding = (Encoding)this.GetEncoding(codepage).Clone();
				encoding.EncoderFallback = encoderFallback;
				encoding.DecoderFallback = decoderFallback;
			}
			return encoding;
		}

		// Token: 0x06006777 RID: 26487 RVA: 0x0015C724 File Offset: 0x0015A924
		internal static void AddProvider(EncodingProvider provider)
		{
			if (provider == null)
			{
				throw new ArgumentNullException("provider");
			}
			object obj = EncodingProvider.s_InternalSyncObject;
			lock (obj)
			{
				if (EncodingProvider.s_providers == null)
				{
					EncodingProvider.s_providers = new EncodingProvider[]
					{
						provider
					};
				}
				else if (Array.IndexOf<EncodingProvider>(EncodingProvider.s_providers, provider) < 0)
				{
					EncodingProvider[] array = new EncodingProvider[EncodingProvider.s_providers.Length + 1];
					Array.Copy(EncodingProvider.s_providers, array, EncodingProvider.s_providers.Length);
					array[array.Length - 1] = provider;
					EncodingProvider.s_providers = array;
				}
			}
		}

		// Token: 0x06006778 RID: 26488 RVA: 0x0015C7D0 File Offset: 0x0015A9D0
		internal static Encoding GetEncodingFromProvider(int codepage)
		{
			if (EncodingProvider.s_providers == null)
			{
				return null;
			}
			EncodingProvider[] array = EncodingProvider.s_providers;
			foreach (EncodingProvider encodingProvider in array)
			{
				Encoding encoding = encodingProvider.GetEncoding(codepage);
				if (encoding != null)
				{
					return encoding;
				}
			}
			return null;
		}

		// Token: 0x06006779 RID: 26489 RVA: 0x0015C818 File Offset: 0x0015AA18
		internal static Encoding GetEncodingFromProvider(string encodingName)
		{
			if (EncodingProvider.s_providers == null)
			{
				return null;
			}
			EncodingProvider[] array = EncodingProvider.s_providers;
			foreach (EncodingProvider encodingProvider in array)
			{
				Encoding encoding = encodingProvider.GetEncoding(encodingName);
				if (encoding != null)
				{
					return encoding;
				}
			}
			return null;
		}

		// Token: 0x0600677A RID: 26490 RVA: 0x0015C860 File Offset: 0x0015AA60
		internal static Encoding GetEncodingFromProvider(int codepage, EncoderFallback enc, DecoderFallback dec)
		{
			if (EncodingProvider.s_providers == null)
			{
				return null;
			}
			EncodingProvider[] array = EncodingProvider.s_providers;
			foreach (EncodingProvider encodingProvider in array)
			{
				Encoding encoding = encodingProvider.GetEncoding(codepage, enc, dec);
				if (encoding != null)
				{
					return encoding;
				}
			}
			return null;
		}

		// Token: 0x0600677B RID: 26491 RVA: 0x0015C8A8 File Offset: 0x0015AAA8
		internal static Encoding GetEncodingFromProvider(string encodingName, EncoderFallback enc, DecoderFallback dec)
		{
			if (EncodingProvider.s_providers == null)
			{
				return null;
			}
			EncodingProvider[] array = EncodingProvider.s_providers;
			foreach (EncodingProvider encodingProvider in array)
			{
				Encoding encoding = encodingProvider.GetEncoding(encodingName, enc, dec);
				if (encoding != null)
				{
					return encoding;
				}
			}
			return null;
		}

		// Token: 0x04002DFD RID: 11773
		private static object s_InternalSyncObject = new object();

		// Token: 0x04002DFE RID: 11774
		private static volatile EncodingProvider[] s_providers;
	}
}
