using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.Prompts.Provisioning;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.ClientAccess.Messages
{
	// Token: 0x0200006F RID: 111
	[Serializable]
	public class ErrorResponse : ResponseBase
	{
		// Token: 0x06000411 RID: 1041 RVA: 0x0000E60F File Offset: 0x0000C80F
		internal ErrorResponse()
		{
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x0000E617 File Offset: 0x0000C817
		internal ErrorResponse(string errorType)
		{
			this.errorType = errorType;
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x0000E626 File Offset: 0x0000C826
		internal ErrorResponse(Exception exception)
		{
			this.errorType = exception.GetType().FullName;
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000414 RID: 1044 RVA: 0x0000E63F File Offset: 0x0000C83F
		// (set) Token: 0x06000415 RID: 1045 RVA: 0x0000E647 File Offset: 0x0000C847
		public string ErrorType
		{
			get
			{
				return this.errorType;
			}
			set
			{
				this.errorType = value;
			}
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x0000E650 File Offset: 0x0000C850
		internal Exception GetException()
		{
			ErrorResponse.CreateExceptionDelegate createExceptionDelegate;
			if (ErrorResponse.errorTable.TryGetValue(this.errorType, out createExceptionDelegate))
			{
				return createExceptionDelegate();
			}
			CallIdTracer.TraceError(ExTraceGlobals.ClientAccessTracer, this, "GetException: Cannot create exception of type {0}.", new object[]
			{
				this.errorType
			});
			return new ClientAccessException();
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x0000E728 File Offset: 0x0000C928
		private static Dictionary<string, ErrorResponse.CreateExceptionDelegate> CreateErrorTable()
		{
			Dictionary<string, ErrorResponse.CreateExceptionDelegate> dictionary = new Dictionary<string, ErrorResponse.CreateExceptionDelegate>();
			dictionary[typeof(IPGatewayNotFoundException).FullName] = (() => new IPGatewayNotFoundException());
			dictionary[typeof(InvalidCallIdException).FullName] = (() => new InvalidCallIdException());
			dictionary[typeof(DialingRulesException).FullName] = (() => new DialingRulesException());
			dictionary[typeof(InvalidPhoneNumberException).FullName] = (() => new InvalidPhoneNumberException());
			dictionary[typeof(InvalidSipUriException).FullName] = (() => new InvalidSipUriException());
			dictionary[typeof(UserNotUmEnabledException).FullName] = (() => new UserNotUmEnabledException(string.Empty));
			dictionary[typeof(OverPlayOnPhoneCallLimitException).FullName] = (() => new OverPlayOnPhoneCallLimitException());
			dictionary[typeof(InvalidFileNameException).FullName] = (() => new InvalidFileNameException(128));
			dictionary[typeof(NoCallerIdToUseException).FullName] = (() => new NoCallerIdToUseException());
			dictionary[typeof(InvalidUMAutoAttendantException).FullName] = (() => new InvalidUMAutoAttendantException());
			dictionary[typeof(AudioDataIsOversizeException).FullName] = (() => new AudioDataIsOversizeException(5, 5L));
			dictionary[typeof(SourceFileNotFoundException).FullName] = (() => new SourceFileNotFoundException(string.Empty));
			dictionary[typeof(DeleteContentException).FullName] = (() => new DeleteContentException(string.Empty));
			dictionary[typeof(PublishingPointException).FullName] = (() => new PublishingPointException(string.Empty));
			dictionary[typeof(EWSUMMailboxAccessException).FullName] = (() => new EWSUMMailboxAccessException(string.Empty));
			return dictionary;
		}

		// Token: 0x040002C4 RID: 708
		private static Dictionary<string, ErrorResponse.CreateExceptionDelegate> errorTable = ErrorResponse.CreateErrorTable();

		// Token: 0x040002C5 RID: 709
		private string errorType;

		// Token: 0x02000070 RID: 112
		// (Invoke) Token: 0x06000429 RID: 1065
		private delegate Exception CreateExceptionDelegate();
	}
}
