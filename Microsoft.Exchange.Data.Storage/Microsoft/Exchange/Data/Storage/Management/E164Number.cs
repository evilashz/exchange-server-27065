using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x020009D1 RID: 2513
	[Serializable]
	public class E164Number : IEquatable<E164Number>, IXmlSerializable
	{
		// Token: 0x06005CBF RID: 23743 RVA: 0x00182B24 File Offset: 0x00180D24
		static E164Number()
		{
			E164Number.e164CcValidityMap["0"] = E164Number.E164CcValidityState.Reserved;
			E164Number.e164CcValidityMap["1"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["20"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["210"] = E164Number.E164CcValidityState.Spare;
			E164Number.e164CcValidityMap["211"] = E164Number.E164CcValidityState.Spare;
			E164Number.e164CcValidityMap["212"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["213"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["214"] = E164Number.E164CcValidityState.Spare;
			E164Number.e164CcValidityMap["215"] = E164Number.E164CcValidityState.Spare;
			E164Number.e164CcValidityMap["216"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["217"] = E164Number.E164CcValidityState.Spare;
			E164Number.e164CcValidityMap["218"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["219"] = E164Number.E164CcValidityState.Spare;
			E164Number.e164CcValidityMap["220"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["221"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["222"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["223"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["224"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["225"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["226"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["227"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["228"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["229"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["230"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["231"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["232"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["233"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["234"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["235"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["236"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["237"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["238"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["239"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["240"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["241"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["242"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["243"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["244"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["245"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["246"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["247"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["248"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["249"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["250"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["251"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["252"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["253"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["254"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["255"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["256"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["257"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["258"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["259"] = E164Number.E164CcValidityState.Spare;
			E164Number.e164CcValidityMap["260"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["261"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["262"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["263"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["264"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["265"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["266"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["267"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["268"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["269"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["27"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["280"] = E164Number.E164CcValidityState.Spare;
			E164Number.e164CcValidityMap["281"] = E164Number.E164CcValidityState.Spare;
			E164Number.e164CcValidityMap["282"] = E164Number.E164CcValidityState.Spare;
			E164Number.e164CcValidityMap["283"] = E164Number.E164CcValidityState.Spare;
			E164Number.e164CcValidityMap["284"] = E164Number.E164CcValidityState.Spare;
			E164Number.e164CcValidityMap["285"] = E164Number.E164CcValidityState.Spare;
			E164Number.e164CcValidityMap["286"] = E164Number.E164CcValidityState.Spare;
			E164Number.e164CcValidityMap["287"] = E164Number.E164CcValidityState.Spare;
			E164Number.e164CcValidityMap["288"] = E164Number.E164CcValidityState.Spare;
			E164Number.e164CcValidityMap["289"] = E164Number.E164CcValidityState.Spare;
			E164Number.e164CcValidityMap["290"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["291"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["292"] = E164Number.E164CcValidityState.Spare;
			E164Number.e164CcValidityMap["293"] = E164Number.E164CcValidityState.Spare;
			E164Number.e164CcValidityMap["294"] = E164Number.E164CcValidityState.Spare;
			E164Number.e164CcValidityMap["295"] = E164Number.E164CcValidityState.Spare;
			E164Number.e164CcValidityMap["296"] = E164Number.E164CcValidityState.Spare;
			E164Number.e164CcValidityMap["297"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["298"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["299"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["30"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["31"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["32"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["33"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["34"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["350"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["351"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["352"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["353"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["354"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["355"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["356"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["357"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["358"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["359"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["36"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["370"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["371"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["372"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["373"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["374"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["375"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["376"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["377"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["378"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["379"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["380"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["381"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["382"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["383"] = E164Number.E164CcValidityState.Spare;
			E164Number.e164CcValidityMap["384"] = E164Number.E164CcValidityState.Spare;
			E164Number.e164CcValidityMap["385"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["386"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["387"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["388"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["389"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["39"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["40"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["41"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["420"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["421"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["422"] = E164Number.E164CcValidityState.Spare;
			E164Number.e164CcValidityMap["423"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["424"] = E164Number.E164CcValidityState.Spare;
			E164Number.e164CcValidityMap["425"] = E164Number.E164CcValidityState.Spare;
			E164Number.e164CcValidityMap["426"] = E164Number.E164CcValidityState.Spare;
			E164Number.e164CcValidityMap["427"] = E164Number.E164CcValidityState.Spare;
			E164Number.e164CcValidityMap["428"] = E164Number.E164CcValidityState.Spare;
			E164Number.e164CcValidityMap["429"] = E164Number.E164CcValidityState.Spare;
			E164Number.e164CcValidityMap["43"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["44"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["45"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["46"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["47"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["48"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["49"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["500"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["501"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["502"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["503"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["504"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["505"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["506"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["507"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["508"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["509"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["51"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["52"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["53"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["54"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["55"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["56"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["57"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["58"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["590"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["591"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["592"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["593"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["594"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["595"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["596"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["597"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["598"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["599"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["60"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["61"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["62"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["63"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["64"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["65"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["66"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["670"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["671"] = E164Number.E164CcValidityState.Spare;
			E164Number.e164CcValidityMap["672"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["673"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["674"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["675"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["676"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["677"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["678"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["679"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["680"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["681"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["682"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["683"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["684"] = E164Number.E164CcValidityState.Spare;
			E164Number.e164CcValidityMap["685"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["686"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["687"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["688"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["689"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["690"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["691"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["692"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["693"] = E164Number.E164CcValidityState.Spare;
			E164Number.e164CcValidityMap["694"] = E164Number.E164CcValidityState.Spare;
			E164Number.e164CcValidityMap["695"] = E164Number.E164CcValidityState.Spare;
			E164Number.e164CcValidityMap["696"] = E164Number.E164CcValidityState.Spare;
			E164Number.e164CcValidityMap["697"] = E164Number.E164CcValidityState.Spare;
			E164Number.e164CcValidityMap["698"] = E164Number.E164CcValidityState.Spare;
			E164Number.e164CcValidityMap["699"] = E164Number.E164CcValidityState.Spare;
			E164Number.e164CcValidityMap["7"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["800"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["801"] = E164Number.E164CcValidityState.Spare;
			E164Number.e164CcValidityMap["802"] = E164Number.E164CcValidityState.Spare;
			E164Number.e164CcValidityMap["803"] = E164Number.E164CcValidityState.Spare;
			E164Number.e164CcValidityMap["804"] = E164Number.E164CcValidityState.Spare;
			E164Number.e164CcValidityMap["805"] = E164Number.E164CcValidityState.Spare;
			E164Number.e164CcValidityMap["806"] = E164Number.E164CcValidityState.Spare;
			E164Number.e164CcValidityMap["807"] = E164Number.E164CcValidityState.Spare;
			E164Number.e164CcValidityMap["808"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["809"] = E164Number.E164CcValidityState.Spare;
			E164Number.e164CcValidityMap["81"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["82"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["830"] = E164Number.E164CcValidityState.Spare;
			E164Number.e164CcValidityMap["831"] = E164Number.E164CcValidityState.Spare;
			E164Number.e164CcValidityMap["832"] = E164Number.E164CcValidityState.Spare;
			E164Number.e164CcValidityMap["833"] = E164Number.E164CcValidityState.Spare;
			E164Number.e164CcValidityMap["834"] = E164Number.E164CcValidityState.Spare;
			E164Number.e164CcValidityMap["835"] = E164Number.E164CcValidityState.Spare;
			E164Number.e164CcValidityMap["836"] = E164Number.E164CcValidityState.Spare;
			E164Number.e164CcValidityMap["837"] = E164Number.E164CcValidityState.Spare;
			E164Number.e164CcValidityMap["838"] = E164Number.E164CcValidityState.Spare;
			E164Number.e164CcValidityMap["839"] = E164Number.E164CcValidityState.Spare;
			E164Number.e164CcValidityMap["84"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["850"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["851"] = E164Number.E164CcValidityState.Spare;
			E164Number.e164CcValidityMap["852"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["853"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["854"] = E164Number.E164CcValidityState.Spare;
			E164Number.e164CcValidityMap["855"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["856"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["857"] = E164Number.E164CcValidityState.Spare;
			E164Number.e164CcValidityMap["858"] = E164Number.E164CcValidityState.Spare;
			E164Number.e164CcValidityMap["859"] = E164Number.E164CcValidityState.Spare;
			E164Number.e164CcValidityMap["86"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["870"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["871"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["872"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["873"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["874"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["875"] = E164Number.E164CcValidityState.Reserved;
			E164Number.e164CcValidityMap["876"] = E164Number.E164CcValidityState.Reserved;
			E164Number.e164CcValidityMap["877"] = E164Number.E164CcValidityState.Reserved;
			E164Number.e164CcValidityMap["878"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["879"] = E164Number.E164CcValidityState.Reserved;
			E164Number.e164CcValidityMap["880"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["881"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["882"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["883"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["884"] = E164Number.E164CcValidityState.Spare;
			E164Number.e164CcValidityMap["885"] = E164Number.E164CcValidityState.Spare;
			E164Number.e164CcValidityMap["886"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["887"] = E164Number.E164CcValidityState.Spare;
			E164Number.e164CcValidityMap["888"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["889"] = E164Number.E164CcValidityState.Spare;
			E164Number.e164CcValidityMap["890"] = E164Number.E164CcValidityState.Spare;
			E164Number.e164CcValidityMap["891"] = E164Number.E164CcValidityState.Spare;
			E164Number.e164CcValidityMap["892"] = E164Number.E164CcValidityState.Spare;
			E164Number.e164CcValidityMap["893"] = E164Number.E164CcValidityState.Spare;
			E164Number.e164CcValidityMap["894"] = E164Number.E164CcValidityState.Spare;
			E164Number.e164CcValidityMap["895"] = E164Number.E164CcValidityState.Spare;
			E164Number.e164CcValidityMap["896"] = E164Number.E164CcValidityState.Spare;
			E164Number.e164CcValidityMap["897"] = E164Number.E164CcValidityState.Spare;
			E164Number.e164CcValidityMap["898"] = E164Number.E164CcValidityState.Spare;
			E164Number.e164CcValidityMap["899"] = E164Number.E164CcValidityState.Spare;
			E164Number.e164CcValidityMap["90"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["91"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["92"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["93"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["94"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["95"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["960"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["961"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["962"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["963"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["964"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["965"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["966"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["967"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["968"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["969"] = E164Number.E164CcValidityState.Reserved;
			E164Number.e164CcValidityMap["970"] = E164Number.E164CcValidityState.Reserved;
			E164Number.e164CcValidityMap["971"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["972"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["973"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["974"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["975"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["976"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["977"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["978"] = E164Number.E164CcValidityState.Spare;
			E164Number.e164CcValidityMap["979"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["98"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["990"] = E164Number.E164CcValidityState.Spare;
			E164Number.e164CcValidityMap["991"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["992"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["993"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["994"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["995"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["996"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["997"] = E164Number.E164CcValidityState.Spare;
			E164Number.e164CcValidityMap["998"] = E164Number.E164CcValidityState.Assigned;
			E164Number.e164CcValidityMap["999"] = E164Number.E164CcValidityState.Reserved;
		}

		// Token: 0x06005CC0 RID: 23744 RVA: 0x00183E88 File Offset: 0x00182088
		private static Exception TryFormulateE164Number(string number, out string formulated)
		{
			formulated = null;
			if (string.IsNullOrEmpty(number) || 3 > number.Length || 65 < number.Length)
			{
				return new FormatException(ServerStrings.ErrorInvalidPhoneNumberFormat);
			}
			number = UnicodeToAnsiConverter.Convert(number, false, '?');
			StringBuilder stringBuilder = new StringBuilder(number.Length);
			bool flag = false;
			int num = 0;
			while (number.Length > num)
			{
				char c = number[num];
				if ((!flag && '+' == c) || char.IsDigit(c))
				{
					flag = true;
					stringBuilder.Append(number[num]);
				}
				else if (!char.IsWhiteSpace(c) && !E164Number.acceptableSymbolsInNumber.Contains((int)c))
				{
					return new FormatException(ServerStrings.ErrorInvalidPhoneNumberFormat);
				}
				num++;
			}
			if (0 < stringBuilder.Length)
			{
				formulated = stringBuilder.ToString().ToLowerInvariant();
				return null;
			}
			return new FormatException(ServerStrings.ErrorInvalidPhoneNumberFormat);
		}

		// Token: 0x06005CC1 RID: 23745 RVA: 0x00183F64 File Offset: 0x00182164
		private static Exception IsE164Number(string number, out string cc, out string sn)
		{
			cc = null;
			sn = null;
			if (string.IsNullOrEmpty(number))
			{
				return new ArgumentNullException("number");
			}
			bool flag = '+' == number[0];
			if ((flag ? 4 : 2) >= number.Length)
			{
				return new ArgumentException("number");
			}
			if (!flag && !char.IsDigit(number[0]))
			{
				return new FormatException(ServerStrings.ErrorInvalidPhoneNumberFormat);
			}
			int num = 1;
			while (number.Length > num)
			{
				if (!char.IsDigit(number[num]))
				{
					return new FormatException(ServerStrings.ErrorInvalidPhoneNumberFormat);
				}
				num++;
			}
			if (!flag)
			{
				sn = number.ToLowerInvariant();
				return null;
			}
			int num2 = 3;
			while (0 < num2)
			{
				string text = number.Substring(1, num2).ToLowerInvariant();
				if (E164Number.e164CcValidityMap.ContainsKey(text))
				{
					cc = text;
					sn = number.Substring(1 + num2).ToLowerInvariant();
					return null;
				}
				num2--;
			}
			return new FormatException(ServerStrings.ErrorInvalidPhoneNumberFormat);
		}

		// Token: 0x06005CC2 RID: 23746 RVA: 0x00184058 File Offset: 0x00182258
		private static Exception IsE164Number(string cc, string sn, out string ccFinal, out string snFinal)
		{
			ccFinal = null;
			snFinal = null;
			if (string.IsNullOrEmpty(sn))
			{
				return new ArgumentNullException("sn");
			}
			foreach (char c in sn)
			{
				if (!char.IsDigit(c))
				{
					return new FormatException(ServerStrings.ErrorInvalidPhoneNumberFormat);
				}
			}
			snFinal = sn.ToLowerInvariant();
			if (string.IsNullOrEmpty(cc))
			{
				return null;
			}
			foreach (char c2 in cc)
			{
				if (!char.IsDigit(c2))
				{
					return new FormatException(ServerStrings.ErrorInvalidPhoneNumberFormat);
				}
			}
			if (E164Number.e164CcValidityMap.ContainsKey(cc))
			{
				ccFinal = cc.ToLowerInvariant();
				return null;
			}
			return new FormatException(ServerStrings.ErrorInvalidPhoneNumberFormat);
		}

		// Token: 0x06005CC3 RID: 23747 RVA: 0x00184134 File Offset: 0x00182334
		public E164Number() : this(null, string.Empty)
		{
			StackTrace stackTrace = new StackTrace();
			foreach (StackFrame stackFrame in stackTrace.GetFrames())
			{
				if (typeof(XmlSerializer) == stackFrame.GetMethod().DeclaringType)
				{
					return;
				}
			}
			throw new InvalidOperationException("the parameterless ctor should only be invoked by System.Xml.Serialization.XmlSerializer");
		}

		// Token: 0x06005CC4 RID: 23748 RVA: 0x00184195 File Offset: 0x00182395
		private E164Number(string cc, string sn)
		{
			this.CountryCode = cc;
			this.SignificantNumber = sn;
		}

		// Token: 0x17001973 RID: 6515
		// (get) Token: 0x06005CC5 RID: 23749 RVA: 0x001841AB File Offset: 0x001823AB
		// (set) Token: 0x06005CC6 RID: 23750 RVA: 0x001841B3 File Offset: 0x001823B3
		public string CountryCode { get; private set; }

		// Token: 0x17001974 RID: 6516
		// (get) Token: 0x06005CC7 RID: 23751 RVA: 0x001841BC File Offset: 0x001823BC
		// (set) Token: 0x06005CC8 RID: 23752 RVA: 0x001841C4 File Offset: 0x001823C4
		public string SignificantNumber { get; private set; }

		// Token: 0x17001975 RID: 6517
		// (get) Token: 0x06005CC9 RID: 23753 RVA: 0x001841D0 File Offset: 0x001823D0
		public string Number
		{
			get
			{
				if (this.number == null)
				{
					this.number = (string.IsNullOrEmpty(this.CountryCode) ? this.SignificantNumber : string.Format("+{0}{1}", this.CountryCode, this.SignificantNumber));
				}
				return this.number;
			}
		}

		// Token: 0x06005CCA RID: 23754 RVA: 0x0018421C File Offset: 0x0018241C
		public static E164Number ParseWithoutFormulating(string number)
		{
			string cc = null;
			string sn = null;
			Exception ex = E164Number.IsE164Number(number, out cc, out sn);
			if (ex != null)
			{
				throw ex;
			}
			return new E164Number(cc, sn);
		}

		// Token: 0x06005CCB RID: 23755 RVA: 0x00184244 File Offset: 0x00182444
		public static E164Number Parse(string number)
		{
			string text = null;
			Exception ex = E164Number.TryFormulateE164Number(number, out text);
			if (ex != null)
			{
				throw ex;
			}
			E164Number e164Number = E164Number.ParseWithoutFormulating(text);
			e164Number.rawNumber = number;
			return e164Number;
		}

		// Token: 0x06005CCC RID: 23756 RVA: 0x00184270 File Offset: 0x00182470
		public static bool TryParseWithoutFormulating(string number, out E164Number e164Number)
		{
			e164Number = null;
			string cc = null;
			string sn = null;
			if (E164Number.IsE164Number(number, out cc, out sn) != null)
			{
				return false;
			}
			e164Number = new E164Number(cc, sn);
			return true;
		}

		// Token: 0x06005CCD RID: 23757 RVA: 0x0018429C File Offset: 0x0018249C
		public static bool TryParse(string number, out E164Number e164Number)
		{
			e164Number = null;
			string text = null;
			return E164Number.TryFormulateE164Number(number, out text) == null && E164Number.TryParseWithoutFormulating(text, out e164Number);
		}

		// Token: 0x06005CCE RID: 23758 RVA: 0x001842C4 File Offset: 0x001824C4
		public static bool IsValidE164Number(string input)
		{
			E164Number e164Number = null;
			return E164Number.TryParse(input, out e164Number);
		}

		// Token: 0x06005CCF RID: 23759 RVA: 0x001842DC File Offset: 0x001824DC
		public static E164Number Parse(string cc, string sn)
		{
			string cc2;
			string sn2;
			Exception ex = E164Number.IsE164Number(cc, sn, out cc2, out sn2);
			if (ex != null)
			{
				throw ex;
			}
			return new E164Number(cc2, sn2);
		}

		// Token: 0x06005CD0 RID: 23760 RVA: 0x00184304 File Offset: 0x00182504
		public static bool TryParse(string cc, string sn, out E164Number e164Number)
		{
			e164Number = null;
			string cc2;
			string sn2;
			if (E164Number.IsE164Number(cc, sn, out cc2, out sn2) != null)
			{
				return false;
			}
			e164Number = new E164Number(cc2, sn2);
			return true;
		}

		// Token: 0x06005CD1 RID: 23761 RVA: 0x0018432D File Offset: 0x0018252D
		public override string ToString()
		{
			return this.rawNumber ?? this.Number;
		}

		// Token: 0x06005CD2 RID: 23762 RVA: 0x0018433F File Offset: 0x0018253F
		public override bool Equals(object obj)
		{
			return this.Equals(obj as E164Number);
		}

		// Token: 0x06005CD3 RID: 23763 RVA: 0x0018434D File Offset: 0x0018254D
		public override int GetHashCode()
		{
			return (this.CountryCode ?? string.Empty).GetHashCode() ^ this.SignificantNumber.GetHashCode();
		}

		// Token: 0x06005CD4 RID: 23764 RVA: 0x0018436F File Offset: 0x0018256F
		public bool Equals(E164Number other)
		{
			return this.Equals(other, false);
		}

		// Token: 0x06005CD5 RID: 23765 RVA: 0x0018437C File Offset: 0x0018257C
		public bool Equals(E164Number other, bool snOnly)
		{
			return !(null == other) && string.Equals(this.SignificantNumber, other.SignificantNumber, StringComparison.OrdinalIgnoreCase) && (snOnly || (string.IsNullOrEmpty(this.CountryCode) && string.IsNullOrEmpty(other.CountryCode)) || string.Equals(this.CountryCode, other.CountryCode, StringComparison.OrdinalIgnoreCase));
		}

		// Token: 0x06005CD6 RID: 23766 RVA: 0x001843DB File Offset: 0x001825DB
		public static bool Equals(E164Number a, E164Number b, bool snOnly)
		{
			if (!object.ReferenceEquals(null, a))
			{
				return a.Equals(b, snOnly);
			}
			return object.ReferenceEquals(null, b) || b.Equals(a, snOnly);
		}

		// Token: 0x06005CD7 RID: 23767 RVA: 0x00184402 File Offset: 0x00182602
		public static bool operator ==(E164Number a, E164Number b)
		{
			return E164Number.Equals(a, b, false);
		}

		// Token: 0x06005CD8 RID: 23768 RVA: 0x0018440C File Offset: 0x0018260C
		public static bool operator !=(E164Number a, E164Number b)
		{
			return !(a == b);
		}

		// Token: 0x06005CD9 RID: 23769 RVA: 0x00184418 File Offset: 0x00182618
		public static bool operator ==(object a, E164Number b)
		{
			return a as E164Number == b;
		}

		// Token: 0x06005CDA RID: 23770 RVA: 0x00184426 File Offset: 0x00182626
		public static bool operator !=(object a, E164Number b)
		{
			return a as E164Number != b;
		}

		// Token: 0x06005CDB RID: 23771 RVA: 0x00184434 File Offset: 0x00182634
		public static bool operator ==(E164Number a, object b)
		{
			return a == b as E164Number;
		}

		// Token: 0x06005CDC RID: 23772 RVA: 0x00184442 File Offset: 0x00182642
		public static bool operator !=(E164Number a, object b)
		{
			return a != b as E164Number;
		}

		// Token: 0x06005CDD RID: 23773 RVA: 0x00184450 File Offset: 0x00182650
		public XmlSchema GetSchema()
		{
			return null;
		}

		// Token: 0x06005CDE RID: 23774 RVA: 0x00184454 File Offset: 0x00182654
		public void ReadXml(XmlReader reader)
		{
			if (XmlNodeType.Element == reader.MoveToContent())
			{
				reader.ReadStartElement();
			}
			if (XmlNodeType.Element == reader.MoveToContent() && string.Equals(reader.Name, "CountryCode", StringComparison.OrdinalIgnoreCase))
			{
				this.CountryCode = reader.ReadElementString("CountryCode");
			}
			if (XmlNodeType.Element == reader.MoveToContent() && string.Equals(reader.Name, "SignificantNumber", StringComparison.OrdinalIgnoreCase))
			{
				this.SignificantNumber = reader.ReadElementString("SignificantNumber");
			}
			if (XmlNodeType.EndElement == reader.MoveToContent())
			{
				reader.ReadEndElement();
			}
		}

		// Token: 0x06005CDF RID: 23775 RVA: 0x001844DA File Offset: 0x001826DA
		public void WriteXml(XmlWriter writer)
		{
			writer.WriteElementString("CountryCode", this.CountryCode);
			writer.WriteElementString("SignificantNumber", this.SignificantNumber);
		}

		// Token: 0x040032FF RID: 13055
		private const string NodeNameCountryCode = "CountryCode";

		// Token: 0x04003300 RID: 13056
		private const string NodeNameSignificantNumber = "SignificantNumber";

		// Token: 0x04003301 RID: 13057
		private static ReadOnlyCollection<int> acceptableSymbolsInNumber = new ReadOnlyCollection<int>(new int[]
		{
			45,
			46,
			40,
			41,
			43
		});

		// Token: 0x04003302 RID: 13058
		private static Dictionary<string, E164Number.E164CcValidityState> e164CcValidityMap = new Dictionary<string, E164Number.E164CcValidityState>();

		// Token: 0x04003303 RID: 13059
		[NonSerialized]
		private string number;

		// Token: 0x04003304 RID: 13060
		[NonSerialized]
		private string rawNumber;

		// Token: 0x020009D2 RID: 2514
		private enum E164CcValidityState
		{
			// Token: 0x04003308 RID: 13064
			Reserved,
			// Token: 0x04003309 RID: 13065
			Assigned,
			// Token: 0x0400330A RID: 13066
			Spare
		}
	}
}
