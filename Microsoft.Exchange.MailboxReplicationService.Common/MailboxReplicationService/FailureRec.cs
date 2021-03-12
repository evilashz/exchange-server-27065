using System;
using System.Xml.Linq;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200010C RID: 268
	[Serializable]
	public sealed class FailureRec : XMLSerializableBase
	{
		// Token: 0x0600096F RID: 2415 RVA: 0x00012C99 File Offset: 0x00010E99
		public FailureRec()
		{
		}

		// Token: 0x06000970 RID: 2416 RVA: 0x00012CA4 File Offset: 0x00010EA4
		private FailureRec(Exception ex)
		{
			this.Timestamp = DateTime.UtcNow;
			this.FailureType = CommonUtils.GetFailureType(ex);
			this.FailureCode = CommonUtils.HrFromException(ex);
			this.MapiLowLevelError = CommonUtils.GetMapiLowLevelError(ex);
			this.FailureSide = CommonUtils.GetExceptionSide(ex);
			this.ExceptionTypes = CommonUtils.ClassifyException(ex);
			this.Message = CommonUtils.FullExceptionMessage(ex).ToString();
			this.DataContext = ExecutionContext.GetDataContext(ex);
			this.StackTrace = CommonUtils.GetStackTrace(ex);
			this.InnerException = FailureRec.Create(ex.InnerException);
		}

		// Token: 0x06000971 RID: 2417 RVA: 0x00012D44 File Offset: 0x00010F44
		private FailureRec(string failureType, string message, string stackTrace, string dataContext, FailureRec innerFailure)
		{
			this.Timestamp = DateTime.UtcNow;
			this.FailureType = failureType;
			this.FailureCode = 0;
			this.MapiLowLevelError = 0;
			this.FailureSide = null;
			WellKnownException[] exceptionTypes = new WellKnownException[1];
			this.ExceptionTypes = exceptionTypes;
			this.Message = message;
			this.StackTrace = stackTrace;
			this.DataContext = dataContext;
			if (innerFailure != null)
			{
				this.InnerException = innerFailure;
			}
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06000972 RID: 2418 RVA: 0x00012DB6 File Offset: 0x00010FB6
		// (set) Token: 0x06000973 RID: 2419 RVA: 0x00012DBE File Offset: 0x00010FBE
		[XmlElement(ElementName = "Timestamp")]
		public DateTime Timestamp { get; set; }

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06000974 RID: 2420 RVA: 0x00012DC7 File Offset: 0x00010FC7
		// (set) Token: 0x06000975 RID: 2421 RVA: 0x00012DCF File Offset: 0x00010FCF
		[XmlElement(ElementName = "FailureType")]
		public string FailureType { get; set; }

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06000976 RID: 2422 RVA: 0x00012DD8 File Offset: 0x00010FD8
		// (set) Token: 0x06000977 RID: 2423 RVA: 0x00012DE0 File Offset: 0x00010FE0
		[XmlElement(ElementName = "FailureCode")]
		public int FailureCode { get; set; }

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06000978 RID: 2424 RVA: 0x00012DE9 File Offset: 0x00010FE9
		// (set) Token: 0x06000979 RID: 2425 RVA: 0x00012DF1 File Offset: 0x00010FF1
		[XmlElement(ElementName = "MapiLowLevelError")]
		public int MapiLowLevelError { get; set; }

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x0600097A RID: 2426 RVA: 0x00012DFA File Offset: 0x00010FFA
		// (set) Token: 0x0600097B RID: 2427 RVA: 0x00012E02 File Offset: 0x00011002
		[XmlIgnore]
		public ExceptionSide? FailureSide { get; private set; }

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x0600097C RID: 2428 RVA: 0x00012E0C File Offset: 0x0001100C
		// (set) Token: 0x0600097D RID: 2429 RVA: 0x00012E34 File Offset: 0x00011034
		[XmlElement(ElementName = "FailureSide")]
		public int FailureSideInt
		{
			get
			{
				ExceptionSide? failureSide = this.FailureSide;
				if (failureSide == null)
				{
					return 0;
				}
				return (int)failureSide.GetValueOrDefault();
			}
			set
			{
				if (value == 0)
				{
					this.FailureSide = null;
					return;
				}
				this.FailureSide = new ExceptionSide?((ExceptionSide)value);
			}
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x0600097E RID: 2430 RVA: 0x00012E60 File Offset: 0x00011060
		// (set) Token: 0x0600097F RID: 2431 RVA: 0x00012E68 File Offset: 0x00011068
		[XmlIgnore]
		public WellKnownException[] ExceptionTypes { get; private set; }

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06000980 RID: 2432 RVA: 0x00012E74 File Offset: 0x00011074
		// (set) Token: 0x06000981 RID: 2433 RVA: 0x00012EC4 File Offset: 0x000110C4
		[XmlElement(ElementName = "ExceptionTypes")]
		public int[] ExceptionTypesInt
		{
			get
			{
				if (this.ExceptionTypes == null || this.ExceptionTypes.Length == 0)
				{
					return null;
				}
				int[] array = new int[this.ExceptionTypes.Length];
				for (int i = 0; i < this.ExceptionTypes.Length; i++)
				{
					array[i] = (int)this.ExceptionTypes[i];
				}
				return array;
			}
			set
			{
				if (value == null || value.Length == 0)
				{
					this.ExceptionTypes = null;
					return;
				}
				this.ExceptionTypes = new WellKnownException[value.Length];
				for (int i = 0; i < value.Length; i++)
				{
					this.ExceptionTypes[i] = (WellKnownException)value[i];
				}
			}
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06000982 RID: 2434 RVA: 0x00012F08 File Offset: 0x00011108
		// (set) Token: 0x06000983 RID: 2435 RVA: 0x00012F10 File Offset: 0x00011110
		[XmlElement(ElementName = "MessageStr")]
		public string Message { get; set; }

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06000984 RID: 2436 RVA: 0x00012F19 File Offset: 0x00011119
		// (set) Token: 0x06000985 RID: 2437 RVA: 0x00012F1C File Offset: 0x0001111C
		[XmlElement(ElementName = "Message")]
		public byte[] MessageData
		{
			get
			{
				return null;
			}
			set
			{
				LocalizedString localizedString = CommonUtils.ByteDeserialize(value);
				if (!localizedString.IsEmpty && string.IsNullOrEmpty(this.Message))
				{
					this.Message = localizedString.ToString();
				}
			}
		}

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06000986 RID: 2438 RVA: 0x00012F59 File Offset: 0x00011159
		// (set) Token: 0x06000987 RID: 2439 RVA: 0x00012F61 File Offset: 0x00011161
		[XmlElement(ElementName = "DataContextStr")]
		public string DataContext { get; set; }

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06000988 RID: 2440 RVA: 0x00012F6A File Offset: 0x0001116A
		// (set) Token: 0x06000989 RID: 2441 RVA: 0x00012F70 File Offset: 0x00011170
		[XmlElement(ElementName = "DataContext")]
		public byte[] DataContextData
		{
			get
			{
				return null;
			}
			set
			{
				LocalizedString localizedString = CommonUtils.ByteDeserialize(value);
				if (!localizedString.IsEmpty && string.IsNullOrEmpty(this.DataContext))
				{
					this.DataContext = localizedString.ToString();
				}
			}
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x0600098A RID: 2442 RVA: 0x00012FAD File Offset: 0x000111AD
		// (set) Token: 0x0600098B RID: 2443 RVA: 0x00012FB5 File Offset: 0x000111B5
		[XmlElement(ElementName = "StackTrace")]
		public string StackTrace { get; set; }

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x0600098C RID: 2444 RVA: 0x00012FBE File Offset: 0x000111BE
		// (set) Token: 0x0600098D RID: 2445 RVA: 0x00012FC6 File Offset: 0x000111C6
		[XmlElement(ElementName = "InnerException")]
		public FailureRec InnerException { get; set; }

		// Token: 0x0600098E RID: 2446 RVA: 0x00012FCF File Offset: 0x000111CF
		public static FailureRec Create(Exception ex)
		{
			if (ex == null)
			{
				return null;
			}
			return new FailureRec(ex);
		}

		// Token: 0x0600098F RID: 2447 RVA: 0x00012FDC File Offset: 0x000111DC
		public static FailureRec Create(string failureType, string message, string stackTrace, string dataContext, string innerException)
		{
			FailureRec innerFailure = string.IsNullOrEmpty(innerException) ? null : FailureRec.Create(string.Empty, string.Empty, innerException, string.Empty, string.Empty);
			return new FailureRec(failureType, message, stackTrace, dataContext, innerFailure);
		}

		// Token: 0x06000990 RID: 2448 RVA: 0x0001301B File Offset: 0x0001121B
		public override string ToString()
		{
			return string.Format("{0}: {1}", this.FailureType, this.Message);
		}

		// Token: 0x06000991 RID: 2449 RVA: 0x00013034 File Offset: 0x00011234
		internal XElement GetDiagnosticData()
		{
			XElement xelement = new XElement("FailureRecord");
			xelement.Add(new XElement("TimeStamp", this.Timestamp));
			xelement.Add(new XElement("DataContext", this.DataContext));
			xelement.Add(new XElement("ExceptionTypes", this.ExceptionTypes));
			xelement.Add(new XElement("FailureType", this.FailureType));
			xelement.Add(new XElement("FailureSide", this.FailureSide));
			xelement.Add(new XElement("FailureCode", this.FailureCode));
			xelement.Add(new XElement("MapiLowLevelError", this.MapiLowLevelError));
			xelement.Add(new XElement("Message", this.Message));
			xelement.Add(new XElement("StackTrace", this.StackTrace));
			if (this.InnerException != null)
			{
				xelement.Add(new XElement("InnerException", this.InnerException.GetDiagnosticData()));
			}
			return xelement;
		}
	}
}
