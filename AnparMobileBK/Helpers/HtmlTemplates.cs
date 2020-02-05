using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnparMobileBK.Helpers
{
    public class HtmlTemplates
    {
        public string ProductTemplate =
            @"<tr>
				<td>{No}</td>
				<td>{Kodu}</td>
				<td>{Tanımı}</td>
				<td>{Olcusu}</td>
				<td>{Miktar}</td>
				<td>{IcMiktar}</td>
				<td>{ListeFiyati}</td>
				<td>{IskontoOranı}</td>
				<td>{BirimFiyat}</td>
				<td>{ToplamTutar}</td>
			</tr>";

        public string Template =
            @"<link href=""https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/css/bootstrap.min.css"" rel=""stylesheet"" integrity=""sha384-Vkoo8x4CGsO3+Hhxv8T/Q5PaXtkKtu6ug5TOeNV6gBiFeWPGFN9MuhOf23Q9Ifjh"" crossorigin=""anonymous"">
    <style>
    tr {
   line-height: 10px;
   min-height: 8px;
   height: 8px;
   font-size:small;

}
td{
    overflow:auto;
    font-size:small;
}
    </style>
<table   width=""100%"" height=""100%"">
    <tr>
        <td height=""10%"">
            <table width=""100%"">
                <tr>
                    <td width=""10%"">
                        <img src=""http://anpar.com.tr/img/logo.png""/>
                    </td>
                    <td align=""center"">
                        <b> ANPAR YAPI MALZEMELERİ 
                       SAN. VE TİC. A.Ş. <br/> SİPARİŞ FORMU<b>
					</td>
				
					<td width=""10%"">
					<img src=""https://st3.myideasoft.com/idea/ah/76/myassets/std_theme_files/tpl_v6_t3_mavi/assets/uploads/logo.jpg?revision=2147483647"" height=""75px"" />
					</td>
				</tr>
			</table>
		</td>
	</tr>

	<tr>
		<td height=""15%"">
			<table width=""100%"" >
				<tr>
					<td width=""33%"" style=""vertical-align: top"" >
						
						<table  width=""100%"" height=""100%"" class=""table table-bordered"" >
							<tr> <th>Firma</th> <td>{Firma}</td> </tr>					
							<tr> <th>Fatura Adresi</th> <td>{FaturaAdresi}</td> </tr>
							<tr> <th>V. Dairesi / No</th> <td><b>{VDairesi}</b></td> </tr>
							<tr> <th>İletişim </th> <td><b>{Iletisim}</b></td> </tr>
							<tr> <th>Sevk Şekli</th> <td align=""center"" style=""background-color: #1f4e78;color:White"">{SevkSekli}</td> </tr>
						</table>

					</td>
					<td style=""vertical-align: top; "" height=""100%"" >
											
						<table  class=""table table-bordered"" >
							<tr align=""center"" style=""background-color: #1f4e78;color:White""> <th>Sevk Adresi</th> </tr>					
							  <tr style=""height: 172px !important;"" align=""center""><td style=""vertical-align: middle;"">{SevkSekli}</td> </tr>					
						</table>
						
					</td>
					<td width=""33%"" style=""vertical-align: top"">
						<table  width=""100%"" height=""100%"" class=""table table-bordered"" >
							<tr height=""10%""align=""center""> <th  style=""background-color: #1f4e78;color:White""colspan=2 >Sipariş Bilgileri</th> </tr>					
							<tr> <th>Tarih</th> <td> {Tarih}</td> </tr>					
							<tr> <th>Plasiyer</th> <td>{Plasiyer}</td> </tr>
							<tr> <th>Sıra No</th> <td>{SıraNo}</td> </tr>
							<tr> <th>Sipariş No </th> <td>{SiparisNo}</td> </tr>
							<tr> <th>Ödeme Vadesi</th> <td>{Vade}</td> </tr>
						</table>
					</td>
				</tr>
			</table>
		</td>
	</tr>

	<tr>
		<td style=""vertical-align:top"">
			<table width=""100%""  class=""table table-bordered""   >
			<tr align=""center"">
				<th width=""2%"">No</th>
				<th>Ürün Kodu</th>
				<th width=""20%"">Ürün Tanımı</th>
				<th>Ürün Ölçüsü (cm)</th>
				<th >Miktar</th >
				<th>Koli İç Miktar</th>
				<th>Liste Fiyatı</th>
				<th>İskonto Oranı</th>
				<th>Net Birim Fiyat</th>
				<th>Toplam Tutar</th>

			</tr>

			{Urunler}		
			</table>

		</td>
	</tr>

	<tr>
		<td height=""10%"" width=""100%"">
			<table width=""100%""   height=""100%"">
				<tr>
					<td width=""50%"" >
						
						 <table  width=""100%"" class=""table table-bordered"" >
							<tr> <td rowspan=""3"" style=""vertical-align: middle; font-weight: bold;"">Taşıma Bilgileri</td> 
								<td>Koli Toplam</td> <td>{KoliToplam}</td> </tr>
							<tr> <td> M<sup>3</sup> Toplam</td> <td>{MToplam}</td> </tr>
							<tr> <td>Toplam Desi</td> <td>{ToplamDesi}</td> </tr>
						 </table>
						
					</td>	
					<td width=""50%"">
						 <table style=""font-weight: bold;"" width=""100%"" class=""table table-bordered"" >
							<tr> <td>Toplam</td> <td>{Toplam} TL</td> </tr>
							<tr> <td>Kdv</td> <td>{KDV} TL</td> </tr>
							<tr> <td>Genel Toplam</td> <td>{GenelToplam} TL</td> </tr>
						 </table>
					</td>
				</tr>
			</table>
		</td>
	</tr>
	
	<tr>
		<td height=""15%"">
			<table width=""100%""   height=""100%"" class=""table table-bordered"" >
				<tr> 
					<td  width=""10%"" rowspan=""3"" style=""vertical-align: middle; font-weight: bold;"">Açıklama</td> 
					<td></td>
				</tr>
				
				<tr> 				
					<td></td>
				</tr>

				<tr> 				
					<td></td>
				</tr>


			</table>
		</td>
	</tr>

</table>";


    }
}
