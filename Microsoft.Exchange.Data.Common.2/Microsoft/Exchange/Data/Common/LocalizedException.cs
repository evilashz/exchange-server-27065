using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Common
{
	// Token: 0x02000009 RID: 9
	[Serializable]
	public class LocalizedException : Exception, ILocalizedException, ILocalizedString
	{
		// Token: 0x06000018 RID: 24 RVA: 0x0000291C File Offset: 0x00000B1C
		internal static void TraceException(string formatString, params object[] formatObjects)
		{
			LocalizedException.TraceExceptionDelegate traceExceptionCallback = LocalizedException.TraceExceptionCallback;
			if (traceExceptionCallback != null)
			{
				LocalizedException.TraceExceptionCallback(formatString, formatObjects);
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002940 File Offset: 0x00000B40
		public LocalizedException(LocalizedString localizedString) : this(localizedString, null)
		{
			LocalizedException.TraceException("Created LocalizedException({0})", new object[]
			{
				localizedString
			});
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002970 File Offset: 0x00000B70
		public LocalizedException(LocalizedString localizedString, Exception innerException) : base(localizedString, innerException)
		{
			this.localizedString = localizedString;
			LocalizedException.TraceException("Created LocalizedException({0}, innerException)", new object[]
			{
				localizedString
			});
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600001B RID: 27 RVA: 0x000029AC File Offset: 0x00000BAC
		public override string Message
		{
			get
			{
				return this.LocalizedString.ToString(this.FormatProvider);
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600001C RID: 28 RVA: 0x000029CD File Offset: 0x00000BCD
		// (set) Token: 0x0600001D RID: 29 RVA: 0x000029D5 File Offset: 0x00000BD5
		public IFormatProvider FormatProvider
		{
			get
			{
				return this.formatProvider;
			}
			set
			{
				this.formatProvider = value;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600001E RID: 30 RVA: 0x000029DE File Offset: 0x00000BDE
		public LocalizedString LocalizedString
		{
			get
			{
				return this.localizedString;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600001F RID: 31 RVA: 0x000029E6 File Offset: 0x00000BE6
		// (set) Token: 0x06000020 RID: 32 RVA: 0x000029EE File Offset: 0x00000BEE
		public int ErrorCode
		{
			get
			{
				return base.HResult;
			}
			set
			{
				base.HResult = value;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000021 RID: 33 RVA: 0x000029F7 File Offset: 0x00000BF7
		public string StringId
		{
			get
			{
				return this.localizedString.StringId;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000022 RID: 34 RVA: 0x00002A04 File Offset: 0x00000C04
		public ReadOnlyCollection<object> StringFormatParameters
		{
			get
			{
				return this.localizedString.FormatParameters;
			}
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002A14 File Offset: 0x00000C14
		public static int GenerateErrorCode(Exception e)
		{
			int num = LocalizedException.InternalGenerateErrorCode(e);
			int num2 = 0;
			if (e.InnerException != null)
			{
				Exception innerException = e.InnerException;
				while (innerException.InnerException != null)
				{
					innerException = innerException.InnerException;
				}
				num2 = LocalizedException.InternalGenerateErrorCode(innerException);
			}
			return num ^ num2;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002A54 File Offset: 0x00000C54
		private static int InternalGenerateErrorCode(Exception e)
		{
			StackTrace stackTrace = new StackTrace(e);
			int hashCode = stackTrace.ToString().GetHashCode();
			int hashCode2 = e.GetType().GetHashCode();
			int num = 0;
			ILocalizedString localizedString = e as ILocalizedString;
			if (localizedString != null)
			{
				num = localizedString.LocalizedString.GetHashCode();
			}
			return num ^ hashCode ^ hashCode2;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002AAA File Offset: 0x00000CAA
		protected LocalizedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.localizedString = (LocalizedString)info.GetValue("localizedString", typeof(LocalizedString));
			LocalizedException.TraceException("Created LocalizedException(info, context)", new object[0]);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002AE4 File Offset: 0x00000CE4
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("localizedString", this.LocalizedString);
		}

		// Token: 0x0400000F RID: 15
		internal static LocalizedException.TraceExceptionDelegate TraceExceptionCallback;

		// Token: 0x04000010 RID: 16
		private IFormatProvider formatProvider;

		// Token: 0x04000011 RID: 17
		private LocalizedString localizedString;

		// Token: 0x0200000A RID: 10
		// (Invoke) Token: 0x06000028 RID: 40
		internal delegate void TraceExceptionDelegate(string formatString, params object[] formatObjects);
	}
}
