using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Authentication;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Security.Principal;
using System.ServiceModel;
using System.ServiceProcess;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace Microsoft.Office.CompliancePolicy
{
	// Token: 0x0200004F RID: 79
	public sealed class GrayException : CompliancePolicyException
	{
		// Token: 0x060001B0 RID: 432 RVA: 0x000060DC File Offset: 0x000042DC
		private GrayException(Exception innerException) : base(string.Format("error message: {0}; original exception call stack: {1}", innerException.Message, innerException.StackTrace), innerException)
		{
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x000060FC File Offset: 0x000042FC
		public static bool DefaultIsGrayExceptionDelegate(Exception e)
		{
			return e is AppDomainUnloadedException || e.GetType() == typeof(ArgumentException) || e is ArgumentNullException || e is ArgumentOutOfRangeException || e is ArithmeticException || e is ArrayTypeMismatchException || e is CannotUnloadAppDomainException || e is KeyNotFoundException || e is InvalidEnumArgumentException || e is WarningException || e is FormatException || e is IndexOutOfRangeException || e is InsufficientMemoryException || e is InvalidCastException || e is InvalidOperationException || e is EndOfStreamException || e is InvalidDataException || e is PathTooLongException || e is MulticastNotSupportedException || e is NotSupportedException || e is NullReferenceException || e is InvalidFilterCriteriaException || e is ExternalException || e is InvalidOleVariantTypeException || e is MarshalDirectiveException || e is SafeArrayRankMismatchException || e is SafeArrayTypeMismatchException || e is SerializationException || e is AuthenticationException || e is CryptographicException || e is PolicyException || e is IdentityNotMappedException || e is SecurityException || e is XmlSyntaxException || e is System.ServiceProcess.TimeoutException || e is DecoderFallbackException || e is EncoderFallbackException || e is System.TimeoutException || e is UnauthorizedAccessException || e is XmlSchemaException || e is XmlException || e is XPathException || e is XsltException || e is CommunicationException;
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x000062CF File Offset: 0x000044CF
		public static void DefaultReportWatsonDelegate(Exception ex)
		{
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x000062D1 File Offset: 0x000044D1
		public static void Initialize(Func<Exception, bool> isGrayExceptionDelegate = null, Action<Exception> reportWatsonDelegate = null)
		{
			if (isGrayExceptionDelegate != null)
			{
				GrayException.isGrayExceptionDelegate = isGrayExceptionDelegate;
			}
			if (reportWatsonDelegate != null)
			{
				GrayException.reportWatsonDelegate = reportWatsonDelegate;
			}
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x000062E5 File Offset: 0x000044E5
		public static void MapAndReportGrayExceptions(Action userCodeDelegate)
		{
			GrayException.MapAndReportGrayExceptions(userCodeDelegate, GrayException.isGrayExceptionDelegate, GrayException.reportWatsonDelegate);
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x000062F8 File Offset: 0x000044F8
		public static void MapAndReportGrayExceptions(Action userCodeDelegate, Func<Exception, bool> isGrayExceptionDelegate, Action<Exception> reportWatsonDelegate)
		{
			ArgumentValidator.ThrowIfNull("userCodeDelegate", userCodeDelegate);
			ArgumentValidator.ThrowIfNull("isGrayExceptionDelegate", isGrayExceptionDelegate);
			ArgumentValidator.ThrowIfNull("reportWatsonDelegate", reportWatsonDelegate);
			try
			{
				userCodeDelegate();
			}
			catch (Exception ex)
			{
				if (isGrayExceptionDelegate(ex))
				{
					reportWatsonDelegate(ex);
					throw new GrayException(ex);
				}
				throw;
			}
		}

		// Token: 0x040000EB RID: 235
		private static Func<Exception, bool> isGrayExceptionDelegate = new Func<Exception, bool>(GrayException.DefaultIsGrayExceptionDelegate);

		// Token: 0x040000EC RID: 236
		private static Action<Exception> reportWatsonDelegate = new Action<Exception>(GrayException.DefaultReportWatsonDelegate);
	}
}
