using System;

namespace Microsoft.Exchange.AirSync.SchemaConverter.Common
{
	// Token: 0x02000197 RID: 407
	internal class UnexpectedTypeException : ConversionException
	{
		// Token: 0x060011A1 RID: 4513 RVA: 0x000609E3 File Offset: 0x0005EBE3
		public UnexpectedTypeException(string expectedType, object actualObject) : this(expectedType, actualObject, true)
		{
		}

		// Token: 0x060011A2 RID: 4514 RVA: 0x000609EE File Offset: 0x0005EBEE
		public UnexpectedTypeException(string expectedType, object actualObject, bool sendInformationalWatson) : base(string.Format("Unexpected type: expected = '{0}', actual = '{1}'", expectedType, (actualObject == null) ? "NULL" : actualObject.GetType().ToString()))
		{
			this.SendInformationalWatson = sendInformationalWatson;
		}

		// Token: 0x060011A3 RID: 4515 RVA: 0x00060A1D File Offset: 0x0005EC1D
		public UnexpectedTypeException(string expectedType, object actualObject, string tagName) : this(expectedType, actualObject, tagName, true)
		{
		}

		// Token: 0x060011A4 RID: 4516 RVA: 0x00060A29 File Offset: 0x0005EC29
		public UnexpectedTypeException(string expectedType, object actualObject, string tagName, bool sendInformationalWatson) : base(string.Format("Unexpected type: expected = '{0}', actual = '{1}', tag name = '{2}'", expectedType, (actualObject == null) ? "NULL" : actualObject.GetType().ToString(), tagName))
		{
			this.SendInformationalWatson = sendInformationalWatson;
		}

		// Token: 0x170006AE RID: 1710
		// (get) Token: 0x060011A5 RID: 4517 RVA: 0x00060A5A File Offset: 0x0005EC5A
		// (set) Token: 0x060011A6 RID: 4518 RVA: 0x00060A62 File Offset: 0x0005EC62
		public bool SendInformationalWatson { get; set; }
	}
}
