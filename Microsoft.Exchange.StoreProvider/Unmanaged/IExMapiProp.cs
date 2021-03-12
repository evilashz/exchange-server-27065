using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x02000290 RID: 656
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IExMapiProp : IExInterface, IDisposeTrackable, IDisposable
	{
		// Token: 0x06000BD4 RID: 3028
		int SaveChanges(int ulFlags);

		// Token: 0x06000BD5 RID: 3029
		int GetProps(ICollection<PropTag> lpPropTags, int ulFlags, out PropValue[] lppPropArray);

		// Token: 0x06000BD6 RID: 3030
		int GetPropList(int ulFlags, out PropTag[] propList);

		// Token: 0x06000BD7 RID: 3031
		int OpenProperty(int propTag, Guid lpInterface, int interfaceOptions, int ulFlags, out IExInterface iObj);

		// Token: 0x06000BD8 RID: 3032
		int SetProps(ICollection<PropValue> lpPropArray, out PropProblem[] lppProblems);

		// Token: 0x06000BD9 RID: 3033
		int DeleteProps(ICollection<PropTag> lpPropTags, out PropProblem[] lppProblems);

		// Token: 0x06000BDA RID: 3034
		int CopyTo(int ciidExclude, Guid[] rgiidExclude, PropTag[] lpExcludeProps, IntPtr ulUiParam, IntPtr lpProgress, Guid lpInterface, IntPtr iMAPIPropDest, int ulFlags, out PropProblem[] lppProblems);

		// Token: 0x06000BDB RID: 3035
		int CopyTo_External(int ciidExclude, Guid[] rgiidExclude, PropTag[] lpExcludeProps, IntPtr ulUiParam, IntPtr lpProgress, Guid lpInterface, IMAPIProp iMAPIPropDest, int ulFlags, out PropProblem[] lppProblems);

		// Token: 0x06000BDC RID: 3036
		int CopyProps(PropTag[] lpIncludeProps, IntPtr ulUIParam, IntPtr lpProgress, Guid lpInterface, IntPtr iMAPIPropDest, int ulFlags, out PropProblem[] lppProblems);

		// Token: 0x06000BDD RID: 3037
		int CopyProps_External(PropTag[] lpIncludeProps, IntPtr ulUIParam, IntPtr lpProgress, Guid lpInterface, IMAPIProp iMAPIPropDest, int ulFlags, out PropProblem[] lppProblems);

		// Token: 0x06000BDE RID: 3038
		int GetNamesFromIDs(ICollection<PropTag> lppPropTagArray, Guid guidPropSet, int ulFlags, out NamedProp[] lppNames);

		// Token: 0x06000BDF RID: 3039
		int GetIDsFromNames(ICollection<NamedProp> lppPropNames, int ulFlags, out PropTag[] lpPropIDs);
	}
}
