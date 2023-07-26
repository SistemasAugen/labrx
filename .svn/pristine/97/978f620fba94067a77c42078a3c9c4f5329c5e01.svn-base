Imports System.IO

Public Class OMAFile
    Dim RADIUS_R() As Integer
    Dim RADIUS_L() As Integer
    Dim ZRADIUS_R() As Integer
    Dim ZRADIUS_L() As Integer
    Dim ACCN As String          'Account Number
    Dim RXNM As String          'Rx Number
    Dim SPH_R As String         'Rx Sphere Power Right
    Dim SPH_L As String         'Rx Sphere Power Left
    Dim CYL_R As String         'Rx Cylinder Power (Diopters) Right
    Dim CYL_L As String         'Rx Cylinder Power (Diopters) Left
    Dim AX_R As String          'Prescribed Cylinder Axis. May be 0-180. (degrees) Right
    Dim AX_L As String          'Prescribed Cylinder Axis. May be 0-180. (degrees) Left
    Dim IPD_R As String         'Monocular centration distance (mm) Right
    Dim IPD_L As String         'Monocular centration distance (mm) Left
    Dim NPD_R As String         'Monocular near centration distance (mm) Right
    Dim NPD_L As String         'Monocular near centration distance (mm) Left
    Dim ADD_R As String         'Addition Power in case the lens is Multifocal, Proggresive or Ejecutive (diopters) Right
    Dim ADD_L As String          'Addition Power in case the lens is Multifocal, Proggresive or Ejecutive (diopters) Left
    Dim SEGHT_R As String       'Vertical SEG Height measured from the (frame) lower boxed tangent Right
    Dim SEGHT_L As String       'Vertical SEG Height measured from the (frame) lower boxed tangent Left
    Dim OCHT_R As String        'Vertical P.R.P height measured from the (frame), lower boxed tangent Right
    Dim OCHT_L As String        'Vertical P.R.P height measured from the (frame), lower boxed tangent Left
    Dim PRVA_R As String        'Rx Prism Base setting 0-360 (degrees) Right
    Dim PRVA_L As String        'Rx Prism Base setting 0-360 (degrees) Left
    Dim PRVM_R As String        'Rx Prism in Diopters, including equithinning prism to inspect at O.C. Right
    Dim PRVM_L As String        'Rx Prism in Diopters, including equithinning prism to inspect at O.C. Left
    Dim HBOX_R As String        'Horizontal Boxed Lens size on frame (mm) Right
    Dim HBOX_L As String        'Horizontal Boxed Lens size on frame (mm) Left
    Dim VBOX_R As String        'Vertical Boxed Lens size (mm) Right
    Dim VBOX_L As String        'Vertical Boxed Lens size (mm) Left
    Dim DBL As String           'Distance Between lenses (mm)
    Dim CIRC_R As String        'Circumference Right
    Dim CIRC_L As String        'Circumference Left
    Dim FMAT As String          'Material of Frame(e.g. "METL")
    Dim ZTILT_R As String       'Side to Side Tilt of Frame as traced Right
    Dim ZTILT_L As String       'Side to Side Tilt of Frame as traced LEft
    Dim PIND_R As String        'The index of prism value, if prism expressed in diopters, otherwise the value should be zero ig prism is expressed in degrees. Right
    Dim PIND_L As String        'The index of prism value, if prism expressed in diopters, otherwise the value should be zero ig prism is expressed in degrees. Left
    Dim LIND_R As String        'Index of Lens Material Right
    Dim LIND_L As String        'Index of Lens Material Left
    Dim LMATID_R As String     'Integer material number, A number agreed to by the device and host to access Material setup tables on both. The value zero is reserved to mean undefined. Right
    Dim LMATID_L As String     'Integer material number, A number agreed to by the device and host to access Material setup tables on both. The value zero is reserved to mean undefined. Left
    Dim LMATNAME_R As String    'Name of Lens Material (e.g. "GLASS","PLASTIC") Right
    Dim LMATNAME_L As String    'Name of Lens Material (e.g. "GLASS","PLASTIC") Left
    Dim LMATTYPE_R As String    'Integer basic material type. Right
    Dim LMATTYPE_L As String    'Integer basic material type. Left
    '   	                        0 - Undefined/Invalid
    '	                            1 - Plastic
    '	                            2 - Polycarbonate
    '	                            3 - Glass
    '	                            4 - Pattern
    ' 	                            5 - Hi-Index
    '	                            6 - Trivex
    '	                            7..127 reserved
    Dim LMFR_R As String        'Blank Manufacturer Right
    Dim LMFR_L As String        'Blank Manufacturer Left
    Dim LNAM_R As String        'Name of lens Style Right
    Dim LNAM_L As String        'Name of lens Style Left
    Dim LTYP_R As String        'Lens Type Right
    Dim LTYP_L As String        'Lens Type Left: Constructed using the following 2 letter codes. All codes that apply
    '                               (up to 4) should be concatenated together and separated by commas. For Example, 
    '                               an aspheric bifocal should be indicated as AS,BI
    '                               	AS - Aspheric
    '                               	AT - Atoric (contact lenses)
    '                               	BI - Bifocal
    '                               	CT - Curve Top
    '                               	DS - Double Segment
    '                               	EX - E-Line Multifocal
    '                                	FT - Flat Top
    '                               	LT - Lenticular
    '                               	PR - Progressive Addition
    '                               	QD - Quadrafocal
    '                               	RD - Round
    '                               	SV - Single Vision
    '                               	TR - Trifocal
    Dim COLR_R As String        'Lens Colour abbreviation Right
    Dim COLR_L As String        'Lens Colour abbreviation Left
    Dim LSIZ_R As Single        'Manufaturer's nominal blank diameter (mm) Right
    Dim LSIZ_L As Single        'Manufaturer's nominal blank diameter (mm) Left
    Dim TINT_R As String        'Tint Abbreviation Right
    Dim TINT_L As String        'Tint Abbreviation Left
    Dim ACOAT_R As String       'Aplied Coating Right
    Dim ACOAT_L As String       'Aplied Coating Left
    Dim DIA_R As String         'Blank Diameter (mm) Right
    Dim DIA_L As String         'Blank Diameter (mm) Left
    Dim MBASE_R As Single       'Nominal lens box top base curve (e.g. 2,4,6, etc) Right
    Dim MBASE_L As Single       'Nominal lens box top base curve (e.g. 2,4,6, etc) Left
    Dim FRNT_R As Single        'Blank True front curve for power calculations (TIND dopters) Right
    Dim FRNT_L As Single        'Blank True front curve for power calculations (TIND dopters) Left
    Dim BACK_R As Single        'Blank Back Curve. (diopters) Right
    Dim BACK_L As Single        'Blank Back Curve. (diopters) Left
    Dim SWIDTH_R As String      'Segment width (mm) Right
    Dim SWIDTH_L As String      'Segment width (mm) Left
    Dim SDEPTH_R As String      'Segment depth (mm) Right
    Dim SDEPTH_L As String      'Segment depth (mm) Left
    Dim OPC_R As String         '10 digit optical product code (OPC) for lenses to be used Right
    Dim OPC_L As String         '10 digit optical product code (OPC) for lenses to be used Left
    Dim SBSGUP_R As String      'Surface Block to layout reference point offsets. Position of the layout reference Right
    Dim SBSGUP_L As String      'Surface Block to layout reference point offsets. Position of the layout reference Left
    Dim SBSGIN_R As String      'point relative to the center of the surface block. Right
    Dim SBSGIN_L As String      'point relative to the center of the surface block. Left
    '                               +IN means LRP is towards nasal relative to the surface block 
    '                               -IN means LRP is towards temporal relative to the surface block 
    '                               +UP means LRP is above the surface block
    '                               -UP means LRP is below the surface block
    Dim SBBCUP_R As String      'Surface block to frame center offsets, i.e. indicates theposition of (Right)
    Dim SBBCUP_L As String      'Surface block to frame center offsets, i.e. indicates theposition of (Left)
    Dim SBBCIN_R As String      'the center of the frame SHAPE relative to the surface blocking position, so that the shape might be shown in its final relative position for determining cutout. Right
    Dim SBBCIN_L As String      'the center of the frame SHAPE relative to the surface blocking position, so that the shape might be shown in its final relative position for determining cutout. Left
    '                               +IN means frame center is towards nasal relative to the surface block
    '                               -IN means frame center is towards temporal relative to the surface block.
    '                               +UP means frame center is above the surface block
    '                               -UP means frame center is below the surface block
    Dim GAX_R As Single         'Cylinder Axis for surfacing machines, 0 - 180 (degrees). Right
    Dim GAX_L As Single         'Cylinder Axis for surfacing machines, 0 - 180 (degrees). Left
    Dim BCOCIN_R As String      'Blank Geometrical Center to optical center IN & Down (mm) Useful for (Right)
    Dim BCOCIN_L As String      'Blank Geometrical Center to optical center IN & Down (mm) Useful for (Left)
    Dim BCOCUP_R As String      'uncuts where frame information is not available. Right
    Dim BCOCUP_L As String      'uncuts where frame information is not available. Left
    '                               +IN means O.C. towards nasal with respect to the geometrical blank center
    '                               -IN means O.C. towards temporal with respect to the geometrical blank center
    Dim BCSGIN_R As String      'Blanck center to segment In & Down i.e. manufaturer's stated segment (Right)
    Dim BCSGIN_L As String      'Blanck center to segment In & Down i.e. manufaturer's stated segment (Left)
    Dim BCSGUP_R As String      'position relative to geometric center of the blank. (Right)
    Dim BCSGUP_L As String      'position relative to geometric center of the blank. (Left)
    '                               +IN Means segment towards nasal with respect to the blank center
    '                               -IN means segment towards temporal with respect to the blank center
    '                               +UP means segment is above the blank center
    '                               -UP means segment is below the blank center 	
    Dim BCTHK_R As Single       'Blank Center Thikness (mm) Right
    Dim BCTHK_L As Single       'Blank Center Thikness (mm) Left
    Dim BETHK_R As Single       'Blank Edge Thinkness (mm) Right
    Dim BETHK_L As Single       'Blank Edge Thinkness (mm) Left
    Dim BPRVA_R As Single       'Base Setting at which BPRVM is located, 0-360 degrees Right
    Dim BPRVA_L As Single       'Base Setting at which BPRVM is located, 0-360 degrees Left
    Dim BPRVM_R As Single       'Amount of prism in semi-finished blank. (degrees or diopters, see record PIND) Right
    Dim BPRVM_L As Single       'Amount of prism in semi-finished blank. (degrees or diopters, see record PIND) Left
    Dim RNGD_R As Single        'Ring Diameter (mm) Right
    Dim RNGD_L As Single        'Ring Diameter (mm) Left
    Dim RNGH_R As Single        'Ring Height (mm) Right
    Dim RNGH_L As Single        'Ring Height (mm) Left
    Dim BLKB_R As Single        'Block Base curve (diopters) Right
    Dim BLKB_L As Single        'Block Base curve (diopters) Left
    Dim BLKD_R As Single        'Block Diameter (mm) Right
    Dim BLKD_L As Single        'Block Diameter (mm) Left
    Dim BLKTYP_R As Integer     'Integer Block Type. A number agreed to by device and host to indicate which of several block tables to use. Can be used to distinguish between different type of materials of block. Right
    Dim BLKTYP_L As Integer     'Integer Block Type. A number agreed to by device and host to indicate which of several block tables to use. Can be used to distinguish between different type of materials of block. Left
    Dim KPRVA_R As Single       'Base setting at which to block KPRVM, 0-360 degrees. Right
    Dim KPRVA_L As Single       'Base setting at which to block KPRVM, 0-360 degrees. Left
    Dim KPRVM_R As Single       'Magnitud of blocked prism.( degrees or diopters, see record PIND) Right
    Dim KPRVM_L As Single       'Magnitud of blocked prism.( degrees or diopters, see record PIND) Left
    Dim TIND_R As Single        'Index of refraction used for all diopter curves in a generator packet Right
    Dim TIND_L As Single        'Index of refraction used for all diopter curves in a generator packet Left
    Dim SBFCUP_R As String      'Surface block to frame center offsets, i.e. indicates the position of Right
    Dim SBFCUP_L As String      'Surface block to frame center offsets, i.e. indicates the position of Left
    Dim SBFCIN_R As String      'the center of the frame SHAPE relative to the surface blocking position, so that the SHAPE may be shown in its final relative position for determining cutout RIGHT
    Dim SBFCIN_L As String      'the center of the frame SHAPE relative to the surface blocking position, so that the SHAPE may be shown in its final relative position for determining cutout Left
    '                               +IN Means frame center is towards nasal with respect to the surface block.
    '                               -IN Means frame center is towards temporal relative to the surface block
    '                               +UP means frame center is above Surface Block
    '                               -UP means frame center is below Surface Block
    Dim GBASE_R As Single       'Generator Base Curve (rounded,TIND Diopters) Right
    Dim GBASE_L As Single       'Generator Base Curve (rounded,TIND Diopters) Left
    Dim GCROS_R As Single       'Generator Cross Curve (rounded,TIND Diopters) Right
    Dim GCROS_L As Single       'Generator Cross Curve (rounded,TIND Diopters) Left
    Dim GBASEX_R As Single      'Generator Base Curve  (unrounded, TIND Diopters) Right
    Dim GBASEX_L As Single      'Generator Base Curve  (unrounded, TIND Diopters) Left
    Dim GCROSX_R As Single      'Generator Cross Curve (unrounded,TIND Diopters) Right
    Dim GCROSX_L As Single      'Generator Cross Curve (unrounded,TIND Diopters) Left
    Dim GTHK_R As Single        'Generator Thickness at center of surface block (mm) Right
    Dim GTHK_L As Single        'Generator Thickness at center of surface block (mm) Left
    Dim FINCMP_R As Single      'Fine Off Allowance compensation (mm) Right
    Dim FINCMP_L As Single      'Fine Off Allowance compensation (mm) Left
    Dim THKCMP_R As Single      'General Purpose Thikness Compensation (mm). This compensation is applied to GTHK Right
    Dim THKCMP_L As Single      'General Purpose Thikness Compensation (mm). This compensation is applied to GTHK Left
    Dim BLKCMP_R As Single      'Generator Thickness compensation for block /lens curve mismatch Right
    Dim BLKCMP_L As Single      'Generator Thickness compensation for block /lens curve mismatch Left
    Dim RNGCMP_R As Single      'Thickness compensation for for prism or blocking ring thickness (mm). this compensation is applied to GTHK. Right
    Dim RNGCMP_L As Single      'Thickness compensation for for prism or blocking ring thickness (mm). this compensation is applied to GTHK. Left
    Dim SAGBD_R As Single       'Sag at blank diameter (mm). Indicates the sag of the lens front 			' surface at its etge prior to cribbing Right
    Dim SAGBD_L As Single       'Sag at blank diameter (mm). Indicates the sag of the lens front 			' surface at its etge prior to cribbing Left
    Dim SAGCD_R As Single       'Sag at blank diameter (mm). Indicates the sag of the lens front 				' surface at its etge after to 			' cribbing Right
    Dim SAGCD_L As Single       'Sag at blank diameter (mm). Indicates the sag of the lens front 				' surface at its etge after to 			' cribbing Left
    Dim SAGRD_R As Single       'Sag at ring diameter (mm). Indicates the sag of the lens front 				' surface in the specified blocking ring diameter (RNGD) Right
    Dim SAGRD_L As Single       'Sag at ring diameter (mm). Indicates the sag of the lens front 				' surface in the specified blocking ring diameter (RNGD) Left
    Dim GPRVA_R As Single       'BASE setting at which to generate GPRVM. 0-360 (degrees) Right
    Dim GPRVA_L As Single       'BASE setting at which to generate GPRVM. 0-360 (degrees) Left
    Dim GPRVM_R As Single       'Amount of Prism to Generate. See also records KPRVM and CPRVM (degrees or diopters, see record PIND) Right
    Dim GPRVM_L As Single       'Amount of Prism to Generate. See also records KPRVM and CPRVM (degrees or diopters, see record PIND) Left
    Dim CPRVA_R As Single       'Meridian at which CPRVM is located, 0-360 degrees Right
    Dim CPRVA_L As Single       'Meridian at which CPRVM is located, 0-360 degrees Left
    Dim CPRVM_R As Single       'Magnitud of compensated prism to grind. Prism is compensated for the effect of 			' tilting the lens off the axis of the cutter. See also recotd GPRVM. Degrees or diopters, see record PIND) Right
    Dim CPRVM_L As Single       'Magnitud of compensated prism to grind. Prism is compensated for the effect of 			' tilting the lens off the axis of the cutter. See also recotd GPRVM. Degrees or diopters, see record PIND) Left
    Dim IFRNT_R As Single       'Blank Implied front curve; equivalente to the SAGRD value converted to a diptric curve based on TIND. Right
    Dim IFRNT_L As Single       'Blank Implied front curve; equivalente to the SAGRD value converted to a diptric curve based on TIND. Left
    Dim OTHK_R As Single        'Generator Thickness at the prism reference point (mm). Used in conjunction with SBOCIN/SBOCUP Right
    Dim OTHK_L As Single        'Generator Thickness at the prism reference point (mm). Used in conjunction with SBOCIN/SBOCUP Left
    Dim SBOCUP_R As String      'Surface Block to prism reference point offsets. Instructs the generator to grind Right
    Dim SBOCUP_L As String      'Surface Block to prism reference point offsets. Instructs the generator to grind Left
    Dim SBOCIN_R As String      'the prism reference point at a position relative to the surface block. Right
    Dim SBOCIN_L As String      'the prism reference point at a position relative to the surface block. Left
    '                               +IN Means PRP is towards nasal relative to the surface block
    '                               -IN Means PRP is towards temporal relative to the surface block
    '                               +UP Means PRP is above the surface block
    '                               -UP Means PRP is below the surface block
    Dim RPRVA_R As Single       'Base Setting at which to generate RPRVM, 0-360 (degrees) Right
    Dim RPRVA_L As Single       'Base Setting at which to generate RPRVM, 0-360 (degrees) Left
    Dim RPRVM_R As Single       'Amount of prism to generate when using the SBOCIN/SBOCUP records for decentration Right
    Dim RPRVM_L As Single       'Amount of prism to generate when using the SBOCIN/SBOCUP records for decentration Left
    Dim CRIB_R As String        'Crib Diameter (mm). A value of Zero will mean no cribbing. A positive value	 				'will mean the crib diameter, a negative value will mean the take off amount (from 			'blank diameter) intended for touching up uncut lenses. Right
    Dim CRIB_L As String        'Crib Diameter (mm). A value of Zero will mean no cribbing. A positive value	 				'will mean the crib diameter, a negative value will mean the take off amount (from 			'blank diameter) intended for touching up uncut lenses. Left
    Dim AVAL_R As Single        'Value indicating the height of a semi-finished blank prior to generating measured relative to the machine's reception chuck on the centerline of the chuck. Right
    Dim AVAL_L As Single        'Value indicating the height of a semi-finished blank prior to generating measured relative to the machine's reception chuck on the centerline of the chuck. Left
    Dim SVAL_R As Single        'Value indicating the thickness of a lens after generating measured relative to the machine's reception chuck on the centerline of the chuck. Right
    Dim SVAL_L As Single        'Value indicating the thickness of a lens after generating measured relative to the machine's reception chuck on the centerline of the chuck. Left
    Dim ELLH_R As Single        'Cribbing ellipse height (mm). Used with the record CRIB to form an ellipse. Right
    Dim ELLH_L As Single        'Cribbing ellipse height (mm). Used with the record CRIB to form an ellipse. Left
    Dim FLATA_R As Single       'Flattening dimension vertical (for square crib (mm). Use with record CRIB. Right
    Dim FLATA_L As Single       'Flattening dimension vertical (for square crib (mm). Use with record CRIB. Left
    Dim FLATB_R As Single       'Flattening dimension horizontal (for square crib (mm). Use with record CRIB. Right
    Dim FLATB_L As Single       'Flattening dimension horizontal (for square crib (mm). Use with record CRIB. Left
    Dim PREEDGE_R As Integer    'Enable=1/Disable=0. Pre-edging of lens by generator. Right
    Dim PREEDGE_L As Integer    'Enable=1/Disable=0. Pre-edging of lens by generator. Left
    Dim PADTHK_R As Single      'Pad Thickness (mm). Right
    Dim PADTHK_L As Single      'Pad Thickness (mm). Left
    Dim LAPBAS_R As Single      'Lap Base Curve (rounded, TIND diopters) Right
    Dim LAPBAS_L As Single      'Lap Base Curve (rounded, TIND diopters) Left
    Dim LAPCRS_R As Single      'Lap Cross Curve (rounded, TIND diopters) Right
    Dim LAPCRS_L As Single      'Lap Cross Curve (rounded, TIND diopters) Left
    Dim LAPBASX_R As Single     'Lap Base Curve (unrounded, TIND diopters) Right
    Dim LAPBASX_L As Single     'Lap Base Curve (unrounded, TIND diopters) Left
    Dim LAPCRSX_R As Single     'Lap Cross Curve (unrounded, TIND diopters) Right
    Dim LAPCRSX_L As Single     'Lap Cross Curve (unrounded, TIND diopters) Left
    Dim LAPM_R As Integer       'Integer lap material number. A number agreed to by device and host to access lap material setup tables. The value zero is reserved to mean undefined. Right
    Dim LAPM_L As Integer       'Integer lap material number. A number agreed to by device and host to access lap material setup tables. The value zero is reserved to mean undefined. Left
    Dim FBFCIN_R As String      'Finish block to frame center offsets. Causes decentering of shape on edger by 					'moving frame center with respect to the finish block. Right
    Dim FBFCIN_L As String      'Finish block to frame center offsets. Causes decentering of shape on edger by 					'moving frame center with respect to the finish block. Left
    Dim FBFCUP_R As String      '+IN means frame center is towards nasal relative to the finish block Right
    Dim FBFCUP_L As String      '+IN means frame center is towards nasal relative to the finish block Left
    Dim FBOCIN_R As String      '-IN means frame center is towards temporal relative to the finish block  Right
    Dim FBOCIN_L As String      '-IN means frame center is towards temporal relative to the finish block  Left
    Dim FBOCUP_R As String      'Finish block to prism reference point offsets (mm). Can be used for single Right
    Dim FBOCUP_L As String      'Finish block to prism reference point offsets (mm). Can be used for single Left
    '                               vision and for multifocals.
    '                               +IN means PRP is towards nasal relative to the finish block
    '                               -IN means PRP is towards temporal relative to the finish block
    '                               +UP means PRP is above the finish block
    '                               -UP means PRP 
    Dim FBSGIN_R As String      'Finish block to layout reference point offsets (mm). if sent for single vision, Right  
    Dim FBSGIN_L As String      'Finish block to layout reference point offsets (mm). if sent for single vision, Left
    Dim FBSGUP_R As String      ' assumed the same as FBOCUP/FBOCIN defined below. Right
    Dim FBSGUP_L As String      ' assumed the same as FBOCUP/FBOCIN defined below. Left
    Dim FCOCIN_R As String      'Frame center to prism reference point offsets, i.e., O.C. Inset and Drop (mm) Right
    Dim FCOCIN_L As String      'Frame center to prism reference point offsets, i.e., O.C. Inset and Drop (mm) Left
    Dim FCOCUP_R As String      ' +IN means PRP is towards nasal with respect to the frame center Right
    Dim FCOCUP_L As String      ' +IN means PRP is towards nasal with respect to the frame center Left
    '                               -IN means PRP is towards temporal with respect to the frame center
    '                               +UP means PRP is above the frame center
    '                               -UP means PRP is below the frame center
    Dim FCSGIN_R As String      'Frame center to layout reference points offset, i.e. seg inset & drop (mm) Right
    Dim FCSGIN_L As String      'Frame center to layout reference points offset, i.e. seg inset & drop (mm) Left
    Dim FCSGUP_R As String      ' +IN means LRP is towards nasal with respect to the frame center Right
    Dim FCSGUP_L As String      ' +IN means LRP is towards nasal with respect to the frame center Left
    '                             -IN means LRP is towards temporal with respect to the frame center
    '                             +UP means LRP is above the frame center
    '                             -UP means LRP is below the frame center
    Dim SGOCIN_R As String      'Layout reference point to prism reference point. Can be used to locate the PRP Right
    Dim SGOCIN_L As String      'Layout reference point to prism reference point. Can be used to locate the PRP Left
    Dim SGOCUP_R As String      ' for multifocals. Right
    Dim SGOCUP_L As String      ' for multifocals. Left
    '                               +IN means PRP is towards nasal relative to the LRP
    '                               -IN means PRP is towards temporal relative to the LRP
    '                               +UP means PRP is above the LRP
    '                               -UP means PRP is below the LRP
    Dim CTHICK_R As Single      'Finished center thickness  (mm at distance O.C.) Right
    Dim CTHICK_L As Single      'Finished center thickness  (mm at distance O.C.) Left
    Dim THKA_R As Single        'Thick point angle -- the meridian at which THKP occurs.(degrees) Right
    Dim THKA_L As Single        'Thick point angle -- the meridian at which THKP occurs.(degrees) Left
    Dim THKP_R As Single        'Thickest edge thickness on finished edge.(mm) Right
    Dim THKP_L As Single        'Thickest edge thickness on finished edge.(mm) Left
    Dim THNA_R As Single        'Thin point angle -- the meridian at which THNP occurs. (degrees) Right
    Dim THNA_L As Single        'Thin point angle -- the meridian at which THNP occurs. (degrees) Left
    Dim THNP_R As Single        'Thinnest edge on finished edge/ (mm) Right
    Dim THNP_L As Single        'Thinnest edge on finished edge/ (mm) Left
    Dim MCIRC As Single         '
    Dim SLBP_R As Single        'Slaboff prism (diopters) Right
    Dim SLBP_L As Single        'Slaboff prism (diopters) Left
    Dim TRCFMT_R As String      'TRACE Data Format Right
    Dim TRCFMT_L As String      'TRACE Data Format Left
    '                               TRCFMT =#;###; E|U; R|L|B; F|P|D
    '                               a) the first field is the format identifier
    '                                   0 - No trace available
    '                                   1 - Basic ASCII radii format 
    '                                   2 - BINARY absolute radii format
    '                                   3 - BINARY differential format
    '                                   4 - PACKED BINARY format
    '                                   5..100 - Reserved for future standad formats 
    '                               b) The second field is the number of radii in which the tracing shall be expressed;
    '                               c) The third field is the radius mode identifier;
    '                                   "E" indicates that radii are evenly spaced ("equiangular");
    '                                   "U" indicates that radii are unevenly spaced , so that an angle data "A" record must follow the "R" record
    '                               d) in initialization or request packets, the fourth field indicates which eye(s) are included:
    '                                   R)ight, 
    '                                   L)eft, 
    '                                   B)oth. In data packets, it specifies the orientation of the tracing 
    '                               e) The fifth field indicates what has been traced: 
    '                                   F)rame, 
    '                                   P)acket 
    '                                   D)emo lens.
    '                               The fifth field is not present when the TRCFMT is sent during initialization.
    '                               All five fields must be sent on subsequent upload and download sessions unless the 			
    '                               first field is 0. In that case, only the first field is sent.
    '                               Only 15 traces per line (R=1;2;3;........;15)
    Dim ZFMT_R As String        'Igual que el TRCFMT Right
    Dim ZFMT_L As String        'Igual que el TRCFMT Left
    Dim DO_Command As String    'B for both, R for Right, L for Left"
    Public ETYP_R As Integer         '1=BEVEL 2=RIMLESS 3=GROOVE
    Public ETYP_L As Integer         '1=BEVEL 2=RIMLESS 3=GROOVE
    Public FTYP As Integer         '1=PLASTIC 2=METAL 3=RIMLESS
    Dim FPINB_R As Single       'The following are in mm - 0 means no pinbevel Right
    Dim FPINB_L As Single       'The following are in mm - 0 means no pinbevel Left
    Dim PINB_R As Single
    Dim PINB_L As Single
    Dim GDEPTH_R As Single
    Dim GDEPTH_L As Single
    Dim GWIDTH_R As Single
    Dim GWIDTH_L As Single
    Dim POLISH As Integer
    Dim CLAMP_R As Integer      'CLAMP ranges from 1 to 10 Right
    Dim CLAMP_L As Integer      'CLAMP ranges from 1 to 10 Left
    Dim DRILL As String         'Drill = R-L-B/x-start/y-start/diameter/x-end/y-end/depth/B-F-A/lateral angle/vertical angle Left
    Dim BEVP_R As String        'BEVC and BEVM are required for certain BEVP 
    Dim BEVP_L As String        'BEVC and BEVM are required for certain BEVP
    Dim BEVC_R As String        'BEVC and BEVM are required for certain BEVP
    Dim BEVC_L As String        'BEVC and BEVM are required for certain BEVP
    '-------------------------------------------------------------------------------------------------------
    ' Las siguientes variables son especificas para la Biseladora MEI
    '-------------------------------------------------------------------------------------------------------
    Dim ANS As Integer          ' 
    Dim JOB As String           '
    Dim STATUS As Integer       '
    Dim _ETYP2_R As Integer     '2nd Edge Type 0) Vertical / 1) Inclined Right
    Dim _ETYP2_L As Integer     '2nd Edge Type 0) Vertical / 1) Inclined Left
    Dim BEVM_R As Single        'Bevel distance from  the front Right
    Dim BEVM_L As Single        'Bevel distance from  the front Left
    Dim _CKBEV_R As Integer     'Wait for a Operator consent before bevel cut Right
    Dim _CKBEV_L As Integer     'Wait for a Operator consent before bevel cut Left
    Dim _FRNT_R As Single       'Front Radius (mm) Right
    Dim _FRNT_L As Single       'Front Radius (mm) Left
    Dim _POLISH2_R As Integer   'Use Polishing 2 Right
    Dim _POLISH2_L As Integer   'Use Polishing 2 Left
    Dim BSIZ_R As Single        'Shape Offset Right
    Dim BSIZ_L As Single        'Shape Offset Left
    Dim _PINONOF_R As Integer   'Enable PINB Right
    Dim _PINONOF_L As Integer   'Enable PINB Left
    Dim _FPINONF_R As Integer   'Enable FPINB 1) The machine make the lower security bevel Right
    Dim _FPINONF_L As Integer   'Enable FPINB 1) The machine make the upper security bevel Left
    Dim _IDSEQ_R As Integer     'Machine ID Work Sequence Right
    Dim _IDSEQ_L As Integer     'Machine ID Work Sequence Left
    Dim _VARINC_R As Integer    'Edging Angle Variations Right
    Dim _VARINC_L As Integer    'Edging Angle Variations Left
    Dim _VANGLE_R As Integer    'Edging Angle Value in Degrees Right
    Dim _VANGLE_L As Integer    'Edging Angle Value in Degrees Left
    Dim _LCOAT_R As Integer     'Coating Slipperiness 0)None 1)Normal 2)Slippy  Right
    Dim _LCOAT_L As Integer     'Coating Slipperiness 0)None 1)Normal 2)Slippy  Left
    Dim _AUTOBFR_R As Integer   'Bevel/Groove Curve Priority 0)Base Frame 1) AUTO 2)SMART     Right
    Dim _AUTOBFR_L As Integer   'Bevel/Groove Curve Priority 0)Base Frame 1) AUTO 2)SMART     Left
    Dim _CKDOCUR As Integer     'Shape Smoothing Function       Disabled/Enabled
    Dim _NDOCUR As Integer      'Advanced Smoothing Function    Disabled/Enabled
    Dim _CURVELV As Integer     'Shape Smoothing Level [1..10]
    Dim _LPRESS_R As Integer    'Lens Holder Pressure (mBar) Right
    Dim _LPRESS_L As Integer    'Lens Holder Pressure (mBar) Left
    Dim _CIRCON_R As Integer    'Keep Perimetry Function Enable Right
    Dim _CIRCON_L As Integer    'Keep Perimetry Function Enable Left
    Dim _FBFCANG_R As Integer   'Angle Rotation in Degrees Right
    Dim _FBFCANG_L As Integer   'Angle Rotation in Degrees Left
    Dim FRAM As String          'File Job Name Saved into the Machine
    Dim FCRV_R As String        'Base of the Frame Right
    Dim FCRV_L As String        'Base of the Frame Left
    Dim _DRILLGR As String      'Group of Each Hole [Outgone]
    Dim DRILLE As String        'Another kind of Drill
    Dim _DRILLREF As String     'Hole Reference on the Edge
    Dim _DRILLLNK As String     'List of indexes of the objects each the hole is connected to
    Dim _CHKRAD_R As Integer    'Interfase use only Right
    Dim _CHKRAD_L As Integer    'Interfase use only Left
    Dim _TBASE_R As String     'Interfase use only Right
    Dim _TBASE_L As String     'Interfase use only Left
    Dim _CHRFRAM As Integer     'Interfase use only 
    Dim _TBASEFR As String      'Interfase use only 
    Dim _CKSEQMAN As Integer    'Interfase use only 
    Dim _COPYDXSX As Integer    'Interfase use only 
    Dim _RMONT_R As Single      'Interfase use only RIGHT
    Dim _RMONT_L As Single      'Interfase use only LEFT

    Dim _VVINCON_L As String
    Dim _VVINCON_R As String
    Dim _VANGLON_L As String
    Dim _VANGLON_R As String
    Dim _VDEPTON_R As String
    Dim _VDEPTON_L As String
    Dim _GANGS_L As String
    Dim _GANGS_R As String
    Dim _GANGE_R As String
    Dim _GANGE_L As String
    Dim _GDEPTH2_L As String
    Dim _GDEPTH2_R As String
    Dim _GWIDTH2_L As String
    Dim _GWIDTH2_R As String
    Dim _GANGS2_R As String
    Dim _GANGS2_L As String
    Dim _GANGE2_R As String
    Dim _GANGE2_L As String
    Dim _GRV2ON_R As String
    Dim _GRV2ON_L As String
    Dim _GRV2SEL_R As String
    Dim _GRV2SEL_L As String


    '-------------------------------------------------------------------------------------------------------
    Enum EdgeType
        NONE = 0
        BEVEL = 1
        RIMLESS = 2
        GROOVE = 3
    End Enum
    Enum FrameMaterial
        Metal
        Plastic
        Zyl
    End Enum
    Enum MaterialName
        Polycarbonate
        Plastic
        Glass
    End Enum
    Enum MaterialType
        Undefined_Invalid = 0
        Plastic = 1
        Polycarbonate = 2
        Glass = 3
        Pattern = 4
        Hi_Index = 5
        Trivex = 6
    End Enum
    Enum Manufacturer
        AUGEN
        SOLA
        TRANSITIONS
        GTX
    End Enum
    Enum LensType
        Aspheric
        Atoric_ContactLenses
        Bifocal
        CurveTop
        DoubleSegment
        ELine_Multifocal
        FlatTop
        Lenticular
        ProgressiveAddition
        Quadrafocal
        Round
        SingleVision
        Trifocal
    End Enum
    Enum EnableStatus
        Disable = 0
        Enable = 1
    End Enum
    Enum TRACEDataFormatIdentifier
        NoTraceAvailable
        BasicASCIIradiiFormat
        BinaryAbsoluteRadiiFormat
        BinaryDifferentialFormat
        PackedBinaryFormat
    End Enum
    Enum TRACERadiusModeIdentifier
        EvenlySpaced
        UnevenlySpaced
    End Enum
    Enum TRACESide
        Right
        Left
        Both
    End Enum
    Enum TRACEObject
        Frame
        Packet
        Demo
    End Enum
    Enum EyeSides
        Both
        Left
        Right
        None
    End Enum
    Enum LensTypes
        Uncut = -1
        Bevel = 1
        Rimless = 2
        Groove = 3
        MiniBevel = 4
        T_Bevel = 33
        InclinedBevelWithStepBack = 34
    End Enum
    Enum FrameTypes
        Plastic = 1
        Metal = 2
        Rimless = 3
    End Enum
    Enum DrillAngleType
        NormalToBack
        NormalToFront
        AngleSpecified
    End Enum
    Enum DrillLocationAndReference
        CenterReference
        EdgeReference
        BoxReference
    End Enum
    Enum DrillFeatureType
        NoneOrType1 = 1
        Type2 = 2
    End Enum
    Enum DrillNasalOrTemporalSide
        Nasal
        Temporal
    End Enum
    Enum DrillMounting
        Front
        Rear
    End Enum
    Enum BevelPossition
        Manual = 0
        FollowFront = 1
        PercentBack = 2
        FrameCurve = 3
        FiftyFifty = 4
        FollowBack = 5
        Automatic = 7
        FreeFloat = 10
    End Enum
    Enum PolishingOptions
        NoPolish = 0
        PolishEdgeAndPinBevels = 1
        PolishPinBevelsOnly = 2
        PolishEdgeOnly = 3
    End Enum
    Enum CoatingSlipperiness
        None = 0
        Normal = 1
        Slippy = 2
    End Enum
    Enum CurvePriority
        BaseFrame = 0
        Auto = 1
        Smart = 2
    End Enum

    Public Sub New(ByVal AccountNumber As String, ByVal Rx_Number As String, ByVal NumRadius As Integer)
        ReDim RADIUS_R(NumRadius)
        ReDim RADIUS_L(NumRadius)
        ACCN = AccountNumber
        RXNM = Rx_Number
        ANS = 20120
        JOB = Rx_Number
        SPH_R = 0
        SPH_L = 0
        CYL_R = 0
        CYL_L = 0
        AX_R = 0
        AX_L = 0
        IPD_R = 0
        IPD_L = 0
        NPD_R = 0
        NPD_L = 0
        ADD_R = 0
        ADD_L = 0
        SEGHT_R = 0
        SEGHT_L = 0
        OCHT_R = 0
        OCHT_L = 0
        PRVA_R = 0
        PRVA_L = 0
        PRVM_R = 0
        PRVM_L = 0
        HBOX_R = 0
        HBOX_L = 0
        VBOX_R = 0
        VBOX_L = 0
        DBL = 0
        CIRC_R = 0
        CIRC_L = 0
        FMAT = ""
        ZTILT_R = 0
        ZTILT_L = 0
        PIND_R = 0
        PIND_L = 0
        LIND_R = 0
        LIND_L = 0
        LMATID_R = 0
        LMATID_L = 0
        LMATNAME_R = ""
        LMATNAME_L = ""
        LMATTYPE_R = ""
        LMATTYPE_L = ""
        LMFR_R = ""
        LMFR_L = ""
        LNAM_R = ""
        LNAM_L = ""
        LTYP_R = ""
        LTYP_L = ""
        COLR_R = ""
        COLR_L = ""
        LSIZ_R = 0
        LSIZ_L = 0
        TINT_R = 0
        TINT_L = 0
        ACOAT_R = ""
        ACOAT_L = ""
        DIA_R = 0
        DIA_L = 0
        MBASE_R = 0
        MBASE_L = 0
        FRNT_R = 0
        FRNT_L = 0
        BACK_R = 0
        BACK_L = 0
        SWIDTH_R = 0
        SWIDTH_L = 0
        SDEPTH_R = 0
        SDEPTH_L = 0
        OPC_R = ""
        OPC_L = ""
        SBSGUP_R = 0
        SBSGUP_L = 0
        SBSGIN_R = 0
        SBSGIN_L = 0
        SBBCUP_R = 0
        SBBCUP_L = 0
        SBBCIN_R = 0
        SBBCIN_L = 0
        GAX_R = 0
        GAX_L = 0
        BCOCIN_R = 0
        BCOCIN_L = 0
        BCOCUP_R = 0
        BCOCUP_L = 0
        BCSGIN_R = 0
        BCSGIN_L = 0
        BCSGUP_R = 0
        BCSGUP_L = 0
        BCTHK_R = 0
        BCTHK_L = 0
        BETHK_R = 0
        BETHK_L = 0
        BPRVA_R = 0
        BPRVA_L = 0
        BPRVM_R = 0
        BPRVM_L = 0
        RNGD_R = 0
        RNGD_L = 0
        RNGH_R = 0
        RNGH_L = 0
        BLKB_R = 0
        BLKB_L = 0
        BLKD_R = 0
        BLKD_L = 0
        BLKTYP_R = 0
        BLKTYP_L = 0
        KPRVA_R = 0
        KPRVA_L = 0
        KPRVM_R = 0
        KPRVM_L = 0
        TIND_R = 0
        TIND_L = 0
        SBFCUP_R = 0
        SBFCUP_L = 0
        SBFCIN_R = 0
        SBFCIN_L = 0
        GBASE_R = 0
        GBASE_L = 0
        GCROS_R = 0
        GCROS_L = 0
        GBASEX_R = 0
        GBASEX_L = 0
        GCROSX_R = 0
        GCROSX_L = 0
        GTHK_R = 0
        GTHK_L = 0
        FINCMP_R = 0
        FINCMP_L = 0
        THKCMP_R = 0
        THKCMP_L = 0
        BLKCMP_R = 0
        BLKCMP_L = 0
        RNGCMP_R = 0
        RNGCMP_L = 0
        SAGBD_R = 0
        SAGBD_L = 0
        SAGCD_R = 0
        SAGCD_L = 0
        SAGRD_R = 0
        SAGRD_L = 0
        GPRVA_R = 0
        GPRVA_L = 0
        GPRVM_R = 0
        GPRVM_L = 0
        CPRVA_R = 0
        CPRVA_L = 0
        CPRVM_R = 0
        CPRVM_L = 0
        IFRNT_R = 0
        IFRNT_L = 0
        OTHK_R = 0
        OTHK_L = 0
        SBOCUP_R = 0
        SBOCUP_L = 0
        SBOCIN_R = 0
        SBOCIN_L = 0
        RPRVA_R = 0
        RPRVA_L = 0
        RPRVM_R = 0
        RPRVM_L = 0
        CRIB_R = 0
        CRIB_L = 0
        AVAL_R = 0
        AVAL_L = 0
        SVAL_R = 0
        SVAL_L = 0
        ELLH_R = 0
        ELLH_L = 0
        FLATA_R = 0
        FLATA_L = 0
        FLATB_R = 0
        FLATB_L = 0
        PREEDGE_R = 0
        PREEDGE_L = 0
        PADTHK_R = 0
        PADTHK_L = 0
        LAPBAS_R = 0
        LAPBAS_L = 0
        LAPCRS_R = 0
        LAPCRS_L = 0
        LAPBASX_R = 0
        LAPBASX_L = 0
        LAPCRSX_R = 0
        LAPCRSX_L = 0
        LAPM_R = 0
        LAPM_L = 0
        FBFCIN_R = 0
        FBFCIN_L = 0
        FBFCUP_R = 0
        FBFCUP_L = 0
        FBOCIN_R = 0
        FBOCIN_L = 0
        FBOCUP_R = 0
        FBOCUP_L = 0
        FBSGIN_R = 0
        FBSGIN_L = 0
        FBSGUP_R = 0
        FBSGUP_L = 0
        FCOCIN_R = 0
        FCOCIN_L = 0
        FCOCUP_R = 0
        FCOCUP_L = 0
        FCSGIN_R = 0
        FCSGIN_L = 0
        FCSGUP_R = 0
        FCSGUP_L = 0
        SGOCIN_R = 0
        SGOCIN_L = 0
        SGOCUP_R = 0
        SGOCUP_L = 0
        CTHICK_R = 0
        CTHICK_L = 0
        THKA_R = 0
        THKA_L = 0
        THKP_R = 0
        THKP_L = 0
        THNA_R = 0
        THNA_L = 0
        THNP_R = 0
        THNP_L = 0
        MCIRC = 0
        SLBP_R = 0
        SLBP_L = 0
        TRCFMT_R = ""
        TRCFMT_L = ""
        ZFMT_R = ""
        ZFMT_L = "'"
        DO_Command = ""
        ETYP_R = 0
        ETYP_L = 0
        FTYP = 0
        FPINB_R = 0
        FPINB_L = 0
        PINB_R = 0
        PINB_L = 0
        GDEPTH_R = 0
        GDEPTH_L = 0
        GWIDTH_R = 0
        GWIDTH_L = 0
        POLISH = 0
        CLAMP_R = 0
        CLAMP_L = 0
        DRILL = ""
        BEVP_R = 0
        BEVP_L = 0
        BEVC_R = 0
        BEVC_L = 0
        FCRV_R = 0
        FCRV_L = 0
    End Sub
    Public Sub SetACCN(ByVal value As String)
        ACCN = value
    End Sub
    Private Function GetACCN() As String
        Return "ACCN=""" & ACCN & """" & vbCrLf
    End Function
    Public Sub SetRXNM(ByVal value As String)
        RXNM = value
    End Sub
    Private Function GetRXNM() As String
        Return "RXNM=""" & RXNM & """" & vbCrLf
    End Function
    Public Sub SetSPH(ByVal Right As Single, ByVal Left As Single)
        SPH_R = Right
        SPH_L = Left
    End Sub
    Private Function GetSPH() As String
        Return "SPH=" & String.Format("{0:f}", SPH_R) & ";" & String.Format("{0:f}", SPH_L) & vbCrLf
    End Function
    Public Sub SetCYL(ByVal Right As Single, ByVal Left As Single)
        CYL_R = Right
        CYL_L = Left
    End Sub
    Private Function GetCYL() As String
        Return "CYL=" & String.Format("{0:f}", CYL_R) & ";" & String.Format("{0:f}", CYL_L) & vbCrLf
    End Function
    Public Sub SetAX(ByVal Right As Single, ByVal Left As Single)
        AX_R = Right
        AX_L = Left
    End Sub
    Private Function GetAX() As String
        Return "AX=" & String.Format("{0:f}", AX_R) & ";" & String.Format("{0:f}", AX_L) & vbCrLf
    End Function
    Public Sub SetIPD(ByVal Right As String, ByVal Left As String)
        IPD_R = Right
        IPD_L = Left
    End Sub
    Public Function GetIPD() As String
        Return "IPD=" & String.Format("{0:f}", IPD_R) & ";" & String.Format("{0:f}", IPD_L) & vbCrLf
    End Function
    Public Sub SetNPD(ByVal Right As String, ByVal Left As String)
        NPD_R = Right
        NPD_L = Left
    End Sub
    Public Function GetNPD() As String
        Return "NPD=" & String.Format("{0:f}", NPD_R) & ";" & String.Format("{0:f}", NPD_L) & vbCrLf
    End Function
    Public Sub SetADD(ByVal Right As Single, ByVal Left As Single)
        ADD_R = Right
        ADD_L = Left
    End Sub
    Private Function GetADD() As String
        Return "ADD=" & String.Format("{0:f}", ADD_R) & ";" & String.Format("{0:f}", ADD_L) & vbCrLf
    End Function
    Public Sub SetSEGHT(ByVal Right As Single, ByVal Left As Single)
        SEGHT_R = Right
        SEGHT_L = Left
    End Sub
    Private Function GetSEGHT() As String
        Return "SEGHT=" & String.Format("{0:f}", SEGHT_R) & ";" & String.Format("{0:f}", SEGHT_L) & vbCrLf
    End Function
    Public Sub SetOCHT(ByVal Right As Single, ByVal Left As Single)
        OCHT_R = Right
        OCHT_L = Left
    End Sub
    Private Function GetOCHT() As String
        Return "OCHT=" & String.Format("{0:f}", OCHT_R) & ";" & String.Format("{0:f}", OCHT_L) & vbCrLf
    End Function
    Public Sub SetPRVA(ByVal Right As Single, ByVal Left As Single)
        PRVA_R = Right
        PRVA_L = Left
    End Sub
    Private Function GetPRVA() As String
        Return "PRVA=" & String.Format("{0:f}", PRVA_R) & ";" & String.Format("{0:f}", PRVA_L) & vbCrLf
    End Function
    Public Sub SetPRVM(ByVal Right As Single, ByVal Left As Single)
        PRVM_R = Right
        PRVM_L = Left
    End Sub
    Private Function GetPRVM() As String
        Return "PRVM=" & String.Format("{0:f}", PRVM_R) & ";" & String.Format("{0:f}", PRVM_L) & vbCrLf
    End Function
    Public Sub SetHBOX(ByVal Right As Single, ByVal Left As Single)
        HBOX_R = Right
        HBOX_L = Left
    End Sub
    Public Function GetHBOX() As String
        Return "HBOX=" & String.Format("{0:f}", HBOX_R) & ";" & String.Format("{0:f}", HBOX_L) & vbCrLf
    End Function
    Public Sub SetVBOX(ByVal Right As Single, ByVal Left As Single)
        VBOX_R = Right
        VBOX_L = Left
    End Sub
    Public Function GetVBOX() As String
        Return "VBOX=" & String.Format("{0:f}", VBOX_R) & ";" & String.Format("{0:f}", VBOX_L) & vbCrLf
    End Function
    Public Sub SetDBL(ByVal DBL As Single)
        Me.DBL = DBL
    End Sub
    Public Function GetDBL() As String
        Return "DBL=" & String.Format("{0:f}", DBL) & vbCrLf
    End Function
    Public Sub SetCIRC(ByVal Right As String, ByVal Left As String)
        CIRC_R = Right
        CIRC_L = Left
    End Sub
    Private Function GetCIRC() As String
        Return "CIRC=" & String.Format("{0:f}", CIRC_R) & ";" & String.Format("{0:f}", CIRC_L) & vbCrLf
    End Function
    Public Sub SetFMAT(ByVal Material As FrameMaterial)
        Select Case Material
            Case FrameMaterial.Metal
                FMAT = "METL"
            Case FrameMaterial.Plastic
                FMAT = "PLAS"
            Case FrameMaterial.Zyl
                FMAT = "Zyl"
        End Select
    End Sub
    Private Function GetFMAT() As String
        Return "FMAT=""" & FMAT & """" & vbCrLf
    End Function
    Public Sub SetZTILT(ByVal Right As Single, ByVal Left As Single)
        ZTILT_R = Right
        ZTILT_L = Left
    End Sub
    Private Function GetZTILT() As String
        Return "ZTILT=" & String.Format("{0:f}", ZTILT_R) & ";" & String.Format("{0:f}", ZTILT_L) & vbCrLf
    End Function
    Public Sub SetPIND(ByVal Right As Single, ByVal Left As Single)
        PIND_R = Right
        PIND_L = Left
    End Sub
    Private Function GetPIND() As String
        Return "PIND=" & String.Format("{0:0.000}", PIND_R) & ";" & String.Format("{0:0.000}", PIND_L) & vbCrLf
    End Function
    Public Sub SetLIND(ByVal Right As Single, ByVal Left As Single)
        LIND_R = Right
        LIND_L = Left
    End Sub
    Private Function GetLIND() As String
        Return "PIND=" & String.Format("{0:0.000}", LIND_R) & ";" & String.Format("{0:0.000}", LIND_L) & vbCrLf
    End Function
    Public Sub SetLMATID(ByVal Right As String, ByVal Left As String)
        LMATID_R = Right
        LMATID_L = Left
    End Sub
    Public Function GetLMATID() As String
        Return "LMATID=" & LMATID_R & ";" & LMATID_L & vbCrLf
    End Function
    Public Sub SetLMATNAME(ByVal RightMaterial As MaterialName, ByVal LeftMaterial As MaterialName)
        Select Case RightMaterial
            Case MaterialName.Glass
                LMATNAME_R = "GLASS"
            Case MaterialName.Plastic
                LMATNAME_R = "PLASTIC"
            Case MaterialName.Polycarbonate
                LMATNAME_R = "POLY"
        End Select
        Select Case LeftMaterial
            Case MaterialName.Glass
                LMATNAME_L = "GLASS"
            Case MaterialName.Plastic
                LMATNAME_L = "PLASTIC"
            Case MaterialName.Polycarbonate
                LMATNAME_L = "POLY"
        End Select
    End Sub
    Public Sub SetLMATNAME(ByVal Right As String, ByVal Left As String)
        LMATNAME_R = Right
        LMATNAME_L = Left
    End Sub
    Private Function GetLMATNAME() As String
        Return "LMATNAME=""" & LMATNAME_R & """;""" & LMATNAME_L & """" & vbCrLf
    End Function
    Public Sub SetLMATTYPE(ByVal RightMaterial As MaterialType, ByVal LeftMaterial As MaterialType)
        LMATTYPE_R = RightMaterial
        LMATTYPE_L = LeftMaterial
    End Sub
    Public Function GetLMATTYPE() As String
        'Return "LMATTYPE=" & LMATTYPE_R & ";" & LMATTYPE_L & vbCrLf
        If DO_Command = "L" Then
            Return "LMATTYPE=" & LMATTYPE_L & vbCrLf
        ElseIf DO_Command = "R" Then
            Return "LMATTYPE=" & LMATTYPE_R & vbCrLf
        Else
            Return "LMATTYPE=" & LMATTYPE_R & ";" & LMATTYPE_L & vbCrLf
        End If
    End Function
    Public Sub SetLMFR(ByVal Right As Manufacturer, ByVal Left As Manufacturer)
        LMFR_R = Right.ToString
        LMFR_L = Left.ToString
    End Sub
    Private Function GetLMFR() As String
        Return "LMFR=""" & LMFR_R & """;""" & LMFR_L & """" & vbCrLf
    End Function
    Public Sub SetLNAM(ByVal Right As LensType, ByVal Left As LensType)
        Select Case Right
            Case LensType.Aspheric
                LNAM_R = "AS"
            Case LensType.Atoric_ContactLenses
                LNAM_R = "AT"
            Case LensType.Bifocal
                LNAM_R = "BI"
            Case LensType.CurveTop
                LNAM_R = "CT"
            Case LensType.DoubleSegment
                LNAM_R = "DS"
            Case LensType.ELine_Multifocal
                LNAM_R = "EX"
            Case LensType.FlatTop
                LNAM_R = "FT"
            Case LensType.Lenticular
                LNAM_R = "LT"
            Case LensType.ProgressiveAddition
                LNAM_R = "PR"
            Case LensType.Quadrafocal
                LNAM_R = "QD"
            Case LensType.Round
                LNAM_R = "RD"
            Case LensType.SingleVision
                LNAM_R = "SV"
            Case LensType.Trifocal
                LNAM_R = "TR"
        End Select
        Select Case Left
            Case LensType.Aspheric
                LNAM_L = "AS"
            Case LensType.Atoric_ContactLenses
                LNAM_L = "AT"
            Case LensType.Bifocal
                LNAM_L = "BI"
            Case LensType.CurveTop
                LNAM_L = "CT"
            Case LensType.DoubleSegment
                LNAM_L = "DS"
            Case LensType.ELine_Multifocal
                LNAM_L = "EX"
            Case LensType.FlatTop
                LNAM_L = "FT"
            Case LensType.Lenticular
                LNAM_L = "LT"
            Case LensType.ProgressiveAddition
                LNAM_L = "PR"
            Case LensType.Quadrafocal
                LNAM_L = "QD"
            Case LensType.Round
                LNAM_L = "RD"
            Case LensType.SingleVision
                LNAM_L = "SV"
            Case LensType.Trifocal
                LNAM_L = "TR"
        End Select
    End Sub
    Public Sub SetLNAM(ByVal Right As String, ByVal Left As String)
        LNAM_R = Right
        LNAM_L = Left
    End Sub
    Private Function GetLNAM() As String
        Return "LNAM=""" & LNAM_R & """;""" & LNAM_L & """" & vbCrLf
    End Function
    Public Sub SetLTYP(ByVal Right As LensType, ByVal Left As LensType)
        Select Case Right
            Case LensType.Aspheric
                LTYP_R = "AS"
            Case LensType.Atoric_ContactLenses
                LTYP_R = "AT"
            Case LensType.Bifocal
                LTYP_R = "BI"
            Case LensType.CurveTop
                LTYP_R = "CT"
            Case LensType.DoubleSegment
                LTYP_R = "DS"
            Case LensType.ELine_Multifocal
                LTYP_R = "EX"
            Case LensType.FlatTop
                LTYP_R = "FT"
            Case LensType.Lenticular
                LTYP_R = "LT"
            Case LensType.ProgressiveAddition
                LTYP_R = "PR"
            Case LensType.Quadrafocal
                LTYP_R = "QD"
            Case LensType.Round
                LTYP_R = "RD"
            Case LensType.SingleVision
                LTYP_R = "SV"
            Case LensType.Trifocal
                LTYP_R = "TR"
        End Select
        Select Case Left
            Case LensType.Aspheric
                LTYP_L = "AS"
            Case LensType.Atoric_ContactLenses
                LTYP_L = "AT"
            Case LensType.Bifocal
                LTYP_L = "BI"
            Case LensType.CurveTop
                LTYP_L = "CT"
            Case LensType.DoubleSegment
                LTYP_L = "DS"
            Case LensType.ELine_Multifocal
                LTYP_L = "EX"
            Case LensType.FlatTop
                LTYP_L = "FT"
            Case LensType.Lenticular
                LTYP_L = "LT"
            Case LensType.ProgressiveAddition
                LTYP_L = "PR"
            Case LensType.Quadrafocal
                LTYP_L = "QD"
            Case LensType.Round
                LTYP_L = "RD"
            Case LensType.SingleVision
                LTYP_L = "SV"
            Case LensType.Trifocal
                LTYP_L = "TR"
        End Select
    End Sub
    Public Sub SetLTYP(ByVal Right As String, ByVal Left As String)
        LTYP_R = Right
        LTYP_L = Left
    End Sub
    Public Function GetLTYP() As String
        Return "LTYP=" & LTYP_R & ";" & LTYP_L & vbCrLf
    End Function
    Public Sub SetCOLR(ByVal Left As String, ByVal Right As String)
        COLR_R = Right
        COLR_L = Left
    End Sub
    Private Function GetCOLR() As String
        Return "COLR=""" & COLR_R & """;""" & COLR_L & """" & vbCrLf
    End Function
    Public Sub SetLSIZ(ByVal Right As Single, ByVal Left As Single)
        LSIZ_R = Right
        LSIZ_L = Left
    End Sub
    Private Function GetLSIZ() As String
        Return "LSIZ=" & String.Format("{0:f}", LSIZ_R) & ";" & String.Format("{0:f}", LSIZ_L) & vbCrLf
    End Function
    Public Sub SetTINT(ByVal Left As String, ByVal Right As String)
        TINT_R = Right
        TINT_L = Left
    End Sub
    Private Function GetTINT() As String
        Return "TINT=""" & TINT_R & """;""" & TINT_L & """" & vbCrLf
    End Function
    Public Sub SetACOAT(ByVal Left As String, ByVal Right As String)
        ACOAT_R = Right
        ACOAT_L = Left
    End Sub
    Private Function GetACOAT() As String
        Return "ACOAT=""" & ACOAT_R & """;""" & ACOAT_L & """" & vbCrLf
    End Function
    Public Sub SetDIA(ByVal Right As String, ByVal Left As String)
        DIA_R = Right
        DIA_L = Left
    End Sub
    Public Sub SetDIA(ByVal value As String)
        Dim temp As String = value.Substring(value.IndexOf("=") + 1, value.IndexOf(";") - value.IndexOf("=") - 1)
        DIA_R = temp
        DIA_L = value.Substring(value.IndexOf(";") + 1)
    End Sub
    Public Function GetDIA() As String
        Return "DIA=" & String.Format("{0:f}", DIA_R) & ";" & String.Format("{0:f}", DIA_L) & vbCrLf
    End Function
    Public Sub SetMBASE(ByVal Right As Single, ByVal Left As Single)
        MBASE_R = Right
        MBASE_L = Left
    End Sub
    Private Function GetMBASE() As String
        Return "MBASE=" & String.Format("{0:f}", MBASE_R) & ";" & String.Format("{0:f}", MBASE_L) & vbCrLf
    End Function
    Public Sub SetFRNT(ByVal Right As Single, ByVal Left As Single)
        FRNT_R = Right
        FRNT_L = Left
    End Sub
    Public Function GetFRNT() As String
        Return "FRNT=" & String.Format("{0:f}", FRNT_R) & ";" & String.Format("{0:f}", FRNT_L) & vbCrLf
    End Function
    Public Sub SetBACK(ByVal Right As Single, ByVal Left As Single)
        BACK_R = Right
        BACK_L = Left
    End Sub
    Private Function GetBACK() As String
        Return "BACK=" & String.Format("{0:f}", BACK_R) & ";" & String.Format("{0:f}", BACK_L) & vbCrLf
    End Function
    Public Sub SetSWIDTH(ByVal Right As String, ByVal Left As String)
        SWIDTH_R = Right
        SWIDTH_L = Left
    End Sub
    Public Sub SetSWIDTH(ByVal value As String)
        Dim temp As String = value.Substring(value.IndexOf("=") + 1, value.IndexOf(";") - value.IndexOf("=") - 1)
        SWIDTH_R = temp
        SWIDTH_L = value.Substring(value.IndexOf(";") + 1)
    End Sub
    Public Function GetSWIDTH() As String
        Return "SWIDTH=" & String.Format("{0:f}", SWIDTH_R) & ";" & String.Format("{0:f}", SWIDTH_L) & vbCrLf
    End Function
    Public Sub SetSDEPTH(ByVal Right As String, ByVal Left As String)
        SDEPTH_R = Right
        SDEPTH_L = Left
    End Sub
    Public Sub SetSDEPTH(ByVal value As String)
        Dim temp As String = value.Substring(value.IndexOf("=") + 1, value.IndexOf(";") - value.IndexOf("=") - 1)
        SDEPTH_R = temp
        SDEPTH_L = value.Substring(value.IndexOf(";") + 1)
    End Sub
    Public Function GetSDEPTH() As String
        Return "SDEPTH=" & String.Format("{0:f}", SDEPTH_R) & ";" & String.Format("{0:f}", SDEPTH_L) & vbCrLf
    End Function
    Public Sub SetOPC(ByVal Right As String, ByVal Left As String)
        OPC_R = Right
        OPC_L = Left
    End Sub
    Private Function GetOPC() As String
        Return "OPC=""" & OPC_R & """;""" & OPC_L & """" & vbCrLf
    End Function
    Public Sub SetSBSGUP(ByVal Right As Single, ByVal Left As Single)
        SBSGUP_R = Right
        SBSGUP_L = Left
    End Sub
    Private Function GetSBSGUP() As String
        Return "SBSGUP=" & String.Format("{0:f}", SBSGUP_R) & ";" & String.Format("{0:f}", SBSGUP_L) & vbCrLf
    End Function
    Public Sub SetSBSGIN(ByVal Right As Single, ByVal Left As Single)
        SBSGIN_R = Right
        SBSGIN_L = Left
    End Sub
    Private Function GetSBSGIN() As String
        Return "SBSGIN=" & String.Format("{0:f}", SBSGIN_R) & ";" & String.Format("{0:f}", SBSGIN_L) & vbCrLf
    End Function
    Public Sub SetSBBCUP(ByVal Right As Single, ByVal Left As Single)
        SBBCUP_R = Right
        SBBCUP_L = Left
    End Sub
    Private Function GetSBBCUP() As String
        Return "SBBCUP=" & String.Format("{0:f}", SBBCUP_R) & ";" & String.Format("{0:f}", SBBCUP_L) & vbCrLf
    End Function
    Public Sub SetSBBCIN(ByVal Right As Single, ByVal Left As Single)
        SBBCIN_R = Right
        SBBCIN_L = Left
    End Sub
    Private Function GetSBBCIN() As String
        Return "SBBCIN=" & String.Format("{0:f}", SBBCIN_R) & ";" & String.Format("{0:f}", SBBCIN_L) & vbCrLf
    End Function
    Public Sub SetGAX(ByVal Right As Single, ByVal Left As Single)
        GAX_R = Right
        GAX_L = Left
    End Sub
    Private Function GetGAX() As String
        Return "GAX=" & String.Format("{0:f}", GAX_R) & ";" & String.Format("{0:f}", GAX_L) & vbCrLf
    End Function
    Public Sub SetBCOCIN(ByVal Right As String, ByVal Left As String)
        BCOCIN_R = Right
        BCOCIN_L = Left
    End Sub
    Public Sub SetBCOCIN(ByVal value As String)
        Dim temp As String = value.Substring(value.IndexOf("=") + 1, value.IndexOf(";") - value.IndexOf("=") - 1)
        BCOCIN_R = temp
        BCOCIN_L = value.Substring(value.IndexOf(";") + 1)
    End Sub
    Public Function GetBCOCIN() As String
        Return "BCOCIN=" & String.Format("{0:f}", BCOCIN_R) & ";" & String.Format("{0:f}", BCOCIN_L) & vbCrLf
    End Function
    Public Sub SetBCOCUP(ByVal Right As String, ByVal Left As String)
        BCOCUP_R = Right
        BCOCUP_L = Left
    End Sub
    Public Sub SetBCOCUP(ByVal value As String)
        Dim temp As String = value.Substring(value.IndexOf("=") + 1, value.IndexOf(";") - value.IndexOf("=") - 1)
        BCOCUP_R = temp
        BCOCUP_L = value.Substring(value.IndexOf(";") + 1)
    End Sub
    Public Function GetBCOCUP() As String
        Return "BCOCUP=" & String.Format("{0:f}", BCOCUP_R) & ";" & String.Format("{0:f}", BCOCUP_L) & vbCrLf
    End Function
    Public Sub SetBCSGIN(ByVal Right As String, ByVal Left As String)
        BCSGIN_R = Right
        BCSGIN_L = Left
    End Sub
    Private Function GetBCSGIN() As String
        Return "BCSGIN=" & String.Format("{0:f}", BCSGIN_R) & ";" & String.Format("{0:f}", BCSGIN_L) & vbCrLf
    End Function
    Public Sub SetBCSGUP(ByVal Right As String, ByVal Left As String)
        BCSGUP_R = Right
        BCSGUP_L = Left
    End Sub
    Private Function GetBCSGUP() As String
        Return "BCSGUP=" & String.Format("{0:f}", BCSGUP_R) & ";" & String.Format("{0:f}", BCSGUP_L) & vbCrLf
    End Function
    Public Sub SetBCTHK(ByVal Right As String, ByVal Left As String)
        BCTHK_R = Right
        BCTHK_L = Left
    End Sub
    Private Function GetBCTHK() As String
        Return "BCTHK=" & String.Format("{0:f}", BCTHK_R) & ";" & String.Format("{0:f}", BCTHK_L) & vbCrLf
    End Function
    Public Sub SetBETHK(ByVal Right As String, ByVal Left As String)
        BETHK_R = Right
        BETHK_L = Left
    End Sub
    Private Function GetBETHK() As String
        Return "BETHK=" & String.Format("{0:f}", BETHK_R) & ";" & String.Format("{0:f}", BETHK_L) & vbCrLf
    End Function
    Public Sub SetBPRVA(ByVal Right As String, ByVal Left As String)
        BPRVA_R = Right
        BPRVA_L = Left
    End Sub
    Private Function GetBPRVA() As String
        Return "BPRVA=" & String.Format("{0:f}", BPRVA_R) & ";" & String.Format("{0:f}", BPRVA_L) & vbCrLf
    End Function
    Public Sub SetBPRVM(ByVal Right As String, ByVal Left As String)
        BPRVM_R = Right
        BPRVM_L = Left
    End Sub
    Private Function GetBPRVM() As String
        Return "BPRVM=" & String.Format("{0:f}", BPRVM_R) & ";" & String.Format("{0:f}", BPRVM_L) & vbCrLf
    End Function
    Public Sub SetRNGD(ByVal Right As String, ByVal Left As String)
        RNGD_R = Right
        RNGD_L = Left
    End Sub
    Private Function GetRNGD() As String
        Return "RNGD=" & String.Format("{0:f}", RNGD_R) & ";" & String.Format("{0:f}", RNGD_L) & vbCrLf
    End Function
    Public Sub SetRNGH(ByVal Right As Single, ByVal Left As Single)
        RNGH_R = Right
        RNGH_L = Left
    End Sub
    Private Function GetRNGH() As String
        Return "RNGH=" & String.Format("{0:f}", RNGH_R) & ";" & String.Format("{0:f}", RNGH_L) & vbCrLf
    End Function
    Public Sub SetBLKB(ByVal Right As Single, ByVal Left As Single)
        BLKB_R = Right
        BLKB_L = Left
    End Sub
    Private Function GetBLKB() As String
        Return "BLKB=" & String.Format("{0:f}", BLKB_R) & ";" & String.Format("{0:f}", BLKB_L) & vbCrLf
    End Function
    Public Sub SetBLKD(ByVal Right As Single, ByVal Left As Single)
        BLKD_R = Right
        BLKD_L = Left
    End Sub
    Private Function GetBLKD() As String
        Return "BLKD=" & String.Format("{0:f}", BLKD_R) & ";" & String.Format("{0:f}", BLKD_L) & vbCrLf
    End Function
    Public Sub SetBLKTYP(ByVal Right As Integer, ByVal Left As Integer)
        BLKTYP_R = Right
        BLKTYP_L = Left
    End Sub
    Private Function GetBLKTYP() As String
        Return "BLKTYP=" & BLKTYP_R & ";" & BLKTYP_L & vbCrLf
    End Function
    Public Sub SetKPRVA(ByVal Right As Single, ByVal Left As Single)
        KPRVA_R = Right
        KPRVA_L = Left
    End Sub
    Private Function GetKPRVA() As String
        Return "KPRVA=" & String.Format("{0:f}", KPRVA_R) & ";" & String.Format("{0:f}", KPRVA_L) & vbCrLf
    End Function
    Public Sub SetKPRVM(ByVal Right As Single, ByVal Left As Single)
        KPRVM_R = Right
        KPRVM_L = Left
    End Sub
    Private Function GetKPRVM() As String
        Return "KPRVM=" & String.Format("{0:f}", KPRVM_R) & ";" & String.Format("{0:f}", KPRVM_L) & vbCrLf
    End Function
    Public Sub SetTIND(ByVal Right As Single, ByVal Left As Single)
        TIND_R = Right
        TIND_L = Left
    End Sub
    Private Function GetTIND() As String
        Return "TIND=" & String.Format("{0:0.000}", TIND_R) & ";" & String.Format("{0:0.000}", TIND_L) & vbCrLf
    End Function
    Public Sub SetSBFCUP(ByVal Right As Single, ByVal Left As Single)
        SBFCUP_R = Right
        SBFCUP_L = Left
    End Sub
    Private Function GetSBFCUP() As String
        Return "SBFCUP=" & String.Format("{0:f}", SBFCUP_R) & ";" & String.Format("{0:f}", SBFCUP_L) & vbCrLf
    End Function
    Public Sub SetSBFCIN(ByVal Right As Single, ByVal Left As Single)
        SBFCIN_R = Right
        SBFCIN_L = Left
    End Sub
    Private Function GetSBFCIN() As String
        Return "SBFCIN=" & String.Format("{0:f}", SBFCIN_R) & ";" & String.Format("{0:f}", SBFCIN_L) & vbCrLf
    End Function
    Public Sub SetGBASE(ByVal Right As Single, ByVal Left As Single)
        GBASE_R = Right
        GBASE_L = Left
    End Sub
    Private Function GetGBASE() As String
        Return "GBASE=" & String.Format("{0:f}", GBASE_R) & ";" & String.Format("{0:f}", GBASE_L) & vbCrLf
    End Function
    Public Sub SetGCROS(ByVal Right As Single, ByVal Left As Single)
        GCROS_R = Right
        GCROS_L = Left
    End Sub
    Private Function GetGCROS() As String
        Return "GCROS=" & String.Format("{0:f}", GCROS_R) & ";" & String.Format("{0:f}", GCROS_L) & vbCrLf
    End Function
    Public Sub SetGBASEX(ByVal Right As Single, ByVal Left As Single)
        GBASEX_R = Right
        GBASEX_L = Left
    End Sub
    Private Function GetGBASEX() As String
        Return "GBASEX=" & String.Format("{0:f}", GBASEX_R) & ";" & String.Format("{0:f}", GBASEX_L) & vbCrLf
    End Function
    Public Sub SetGCROSX(ByVal Right As Single, ByVal Left As Single)
        GCROSX_R = Right
        GCROSX_L = Left
    End Sub
    Private Function GetGCROSX() As String
        Return "GCROSX=" & String.Format("{0:f}", GCROSX_R) & ";" & String.Format("{0:f}", GCROSX_L) & vbCrLf
    End Function
    Public Sub SetGTHK(ByVal Right As Single, ByVal Left As Single)
        GTHK_R = Right
        GTHK_L = Left
    End Sub
    Private Function GetGTHK() As String
        Return "GTHK=" & String.Format("{0:f}", GTHK_R) & ";" & String.Format("{0:f}", GTHK_L) & vbCrLf
    End Function
    Public Sub SetFINCMP(ByVal Right As Single, ByVal Left As Single)
        FINCMP_R = Right
        FINCMP_L = Left
    End Sub
    Private Function GetFINCMP() As String
        Return "FINCMP=" & String.Format("{0:f}", FINCMP_R) & ";" & String.Format("{0:f}", FINCMP_L) & vbCrLf
    End Function
    Public Sub SetTHKCMP(ByVal Right As Single, ByVal Left As Single)
        THKCMP_R = Right
        THKCMP_L = Left
    End Sub
    Private Function GetTHKCMP() As String
        Return "THKCMP=" & String.Format("{0:f}", THKCMP_R) & ";" & String.Format("{0:f}", THKCMP_L) & vbCrLf
    End Function
    Public Sub SetBLKCMP(ByVal Right As Single, ByVal Left As Single)
        BLKCMP_R = Right
        BLKCMP_L = Left
    End Sub
    Private Function GetBLKCMP() As String
        Return "BLKCMP=" & String.Format("{0:f}", BLKCMP_R) & ";" & String.Format("{0:f}", BLKCMP_L) & vbCrLf
    End Function
    Public Sub SetRNGCMP(ByVal Right As Single, ByVal Left As Single)
        RNGCMP_R = Right
        RNGCMP_L = Left
    End Sub
    Private Function GetRNGCMP() As String
        Return "RNGCMP=" & String.Format("{0:f}", RNGCMP_R) & ";" & String.Format("{0:f}", RNGCMP_L) & vbCrLf
    End Function
    Public Sub SetSAGBD(ByVal Right As Single, ByVal Left As Single)
        SAGBD_R = Right
        SAGBD_L = Left
    End Sub
    Private Function GetSAGBD() As String
        Return "SAGBD=" & String.Format("{0:f}", SAGBD_R) & ";" & String.Format("{0:f}", SAGBD_L) & vbCrLf
    End Function
    Public Sub SetSAGCD(ByVal Right As Single, ByVal Left As Single)
        SAGCD_R = Right
        SAGCD_L = Left
    End Sub
    Private Function GetSAGCD() As String
        Return "SAGCD=" & String.Format("{0:f}", SAGCD_R) & ";" & String.Format("{0:f}", SAGCD_L) & vbCrLf
    End Function
    Public Sub SetSAGRD(ByVal Right As Single, ByVal Left As Single)
        SAGRD_R = Right
        SAGRD_L = Left
    End Sub
    Private Function GetSAGRD() As String
        Return "SAGRD=" & String.Format("{0:f}", SAGRD_R) & ";" & String.Format("{0:f}", SAGRD_L) & vbCrLf
    End Function
    Public Sub SetGPRVA(ByVal Right As Single, ByVal Left As Single)
        GPRVA_R = Right
        GPRVA_L = Left
    End Sub
    Private Function GetGPRVA() As String
        Return "GPRVA=" & String.Format("{0:f}", GPRVA_R) & ";" & String.Format("{0:f}", GPRVA_L) & vbCrLf
    End Function
    Public Sub SetGPRVM(ByVal Right As Single, ByVal Left As Single)
        GPRVM_R = Right
        GPRVM_L = Left
    End Sub
    Private Function GetGPRVM() As String
        Return "GPRVM=" & String.Format("{0:f}", GPRVM_R) & ";" & String.Format("{0:f}", GPRVM_L) & vbCrLf
    End Function
    Public Sub SetCPRVA(ByVal Right As Single, ByVal Left As Single)
        CPRVA_R = Right
        CPRVA_L = Left
    End Sub
    Private Function GetCPRVA() As String
        Return "CPRVA=" & String.Format("{0:f}", CPRVA_R) & ";" & String.Format("{0:f}", CPRVA_L) & vbCrLf
    End Function
    Public Sub SetCPRVM(ByVal Right As Single, ByVal Left As Single)
        CPRVM_R = Right
        CPRVM_L = Left
    End Sub
    Private Function GetCPRVM() As String
        Return "CPRVM=" & String.Format("{0:f}", CPRVM_R) & ";" & String.Format("{0:f}", CPRVM_L) & vbCrLf
    End Function
    Public Sub SetIFRNT(ByVal Right As Single, ByVal Left As Single)
        IFRNT_R = Right
        IFRNT_L = Left
    End Sub
    Private Function GetIFRNT() As String
        Return "IFRNT=" & String.Format("{0:f}", IFRNT_R) & ";" & String.Format("{0:f}", IFRNT_L) & vbCrLf
    End Function
    Public Sub SetOTHK(ByVal Right As Single, ByVal Left As Single)
        OTHK_R = Right
        OTHK_L = Left
    End Sub
    Private Function GetOTHK() As String
        Return "OTHK=" & String.Format("{0:f}", OTHK_R) & ";" & String.Format("{0:f}", OTHK_L) & vbCrLf
    End Function
    Public Sub SetSBOCUP(ByVal Right As Single, ByVal Left As Single)
        SBOCUP_R = Right
        SBOCUP_L = Left
    End Sub
    Private Function GetSBOCUP() As String
        Return "SBOCUP=" & String.Format("{0:f}", SBOCUP_R) & ";" & String.Format("{0:f}", SBOCUP_L) & vbCrLf
    End Function
    Public Sub SetSBOCIN(ByVal Right As Single, ByVal Left As Single)
        SBOCIN_R = Right
        SBOCIN_L = Left
    End Sub
    Private Function GetSBOCIN() As String
        Return "SBOCIN=" & String.Format("{0:f}", SBOCIN_R) & ";" & String.Format("{0:f}", SBOCIN_L) & vbCrLf
    End Function
    Public Sub SetRPRVA(ByVal Right As Single, ByVal Left As Single)
        RPRVA_R = Right
        RPRVA_L = Left
    End Sub
    Private Function GetRPRVA() As String
        Return "RPRVA=" & String.Format("{0:f}", RPRVA_R) & ";" & String.Format("{0:f}", RPRVA_L) & vbCrLf
    End Function
    Public Sub SetRPRVM(ByVal Right As Single, ByVal Left As Single)
        RPRVM_R = Right
        RPRVM_L = Left
    End Sub
    Private Function GetRPRVM() As String
        Return "RPRVM=" & String.Format("{0:f}", RPRVM_R) & ";" & String.Format("{0:f}", RPRVM_L) & vbCrLf
    End Function
    Public Sub SetCRIB(ByVal Right As String, ByVal Left As String)
        CRIB_R = Right
        CRIB_L = Left
    End Sub
    Public Function GetCRIB() As String
        Return "CRIB=" & String.Format("{0:f}", CRIB_R) & ";" & String.Format("{0:f}", CRIB_L) & vbCrLf
    End Function
    Public Sub SetAVAL(ByVal Right As Single, ByVal Left As Single)
        AVAL_R = Right
        AVAL_L = Left
    End Sub
    Private Function GetAVAL() As String
        Return "AVAL=" & String.Format("{0:f}", AVAL_R) & ";" & String.Format("{0:f}", AVAL_L) & vbCrLf
    End Function
    Public Sub SetSVAL(ByVal Right As Single, ByVal Left As Single)
        SVAL_R = Right
        SVAL_L = Left
    End Sub
    Private Function GetSVAL() As String
        Return "SVAL=" & String.Format("{0:f}", SVAL_R) & ";" & String.Format("{0:f}", SVAL_L) & vbCrLf
    End Function
    Public Sub SetELLH(ByVal Right As Single, ByVal Left As Single)
        ELLH_R = Right
        ELLH_L = Left
    End Sub
    Private Function GetELLH() As String
        Return "ELLH=" & String.Format("{0:f}", ELLH_R) & ";" & String.Format("{0:f}", ELLH_L) & vbCrLf
    End Function
    Public Sub SetFLATA(ByVal Right As Single, ByVal Left As Single)
        FLATA_R = Right
        FLATA_L = Left
    End Sub
    Private Function GetFLATA() As String
        Return "FLATA=" & String.Format("{0:f}", FLATA_R) & ";" & String.Format("{0:f}", FLATA_L) & vbCrLf
    End Function
    Public Sub SetFLATB(ByVal Right As Single, ByVal Left As Single)
        FLATB_R = Right
        FLATB_L = Left
    End Sub
    Private Function GetFLATB() As String
        Return "FLATB=" & String.Format("{0:f}", FLATB_R) & ";" & String.Format("{0:f}", FLATB_L) & vbCrLf
    End Function
    Public Sub SetPREEDGE(ByVal Right As EnableStatus, ByVal Left As EnableStatus)
        PREEDGE_R = Right
        PREEDGE_L = Left
    End Sub
    Private Function GetPREEDGE() As String
        Return "PREEDGE=" & PREEDGE_R & ";" & PREEDGE_L & vbCrLf
    End Function
    Public Sub SetPADTHK(ByVal Right As Single, ByVal Left As Single)
        PADTHK_R = Right
        PADTHK_L = Left
    End Sub
    Private Function GetPADTHK() As String
        Return "PADTHK=" & String.Format("{0:f}", PADTHK_R) & ";" & String.Format("{0:f}", PADTHK_L) & vbCrLf
    End Function
    Public Sub SetLAPBAS(ByVal Right As Single, ByVal Left As Single)
        LAPBAS_R = Right
        LAPBAS_L = Left
    End Sub
    Private Function GetLAPBAS() As String
        Return "LAPBAS=" & String.Format("{0:f}", LAPBAS_R) & ";" & String.Format("{0:f}", LAPBAS_L) & vbCrLf
    End Function
    Public Sub SetLAPCRS(ByVal Right As Single, ByVal Left As Single)
        LAPCRS_R = Right
        LAPCRS_L = Left
    End Sub
    Private Function GetLAPCRS() As String
        Return "LAPCRS=" & String.Format("{0:f}", LAPCRS_R) & ";" & String.Format("{0:f}", LAPCRS_L) & vbCrLf
    End Function
    Public Sub SetLAPBASX(ByVal Right As Single, ByVal Left As Single)
        LAPBASX_R = Right
        LAPBASX_L = Left
    End Sub
    Private Function GetLAPBASX() As String
        Return "LAPBASX=" & String.Format("{0:f}", LAPBASX_R) & ";" & String.Format("{0:f}", LAPBASX_L) & vbCrLf
    End Function
    Public Sub SetLAPCRSX(ByVal Right As Single, ByVal Left As Single)
        LAPCRSX_R = Right
        LAPCRSX_L = Left
    End Sub
    Private Function GetLAPCRSX() As String
        Return "LAPCRSX=" & String.Format("{0:f}", LAPCRSX_R) & ";" & String.Format("{0:f}", LAPCRSX_L) & vbCrLf
    End Function
    Public Sub SetLAPM(ByVal Right As String, ByVal Left As String)
        LAPM_R = Right
        LAPM_L = Left
    End Sub
    Private Function GetLAPM() As String
        Return "LAPM=" & String.Format("{0:f}", LAPM_R) & ";" & String.Format("{0:f}", LAPM_L) & vbCrLf
    End Function
    Public Sub SetFBFCIN(ByVal Right As String, ByVal Left As String)
        FBFCIN_R = Right
        FBFCIN_L = Left
    End Sub
    Private Function GetFBFCIN() As String
        Return "FBFCIN=" & String.Format("{0:f}", FBFCIN_R) & ";" & String.Format("{0:f}", FBFCIN_L) & vbCrLf
    End Function
    Public Sub SetFBFCUP(ByVal Right As String, ByVal Left As String)
        FBFCUP_R = Right
        FBFCUP_L = Left
    End Sub
    Private Function GetFBFCUP() As String
        Return "FBFCUP=" & String.Format("{0:f}", FBFCUP_R) & ";" & String.Format("{0:f}", FBFCUP_L) & vbCrLf
    End Function
    Public Sub SetFBOCIN(ByVal Right As String, ByVal Left As String)
        FBOCIN_R = Right
        FBOCIN_L = Left
    End Sub
    Public Sub SetFBOCIN(ByVal value As String)
        FBOCIN_R = value.Replace("FBOCIN=", "")
        FBOCIN_L = value.Replace("FBOCIN=", "")
    End Sub
    Private Function GetFBOCIN() As String
        Return "FBOCIN=" & String.Format("{0:f}", FBOCIN_R) & ";" & String.Format("{0:f}", FBOCIN_L) & vbCrLf
    End Function
    Public Sub SetFBOCUP(ByVal Right As String, ByVal Left As String)
        FBOCUP_R = Right
        FBOCUP_L = Left
    End Sub
    Public Sub SetFBOCUP(ByVal value As String)
        FBOCUP_R = value.Replace("FBOCUP=", "")
        FBOCUP_L = value.Replace("FBOCUP=", "")
    End Sub
    Private Function GetFBOCUP() As String
        Return "FBOCUP=" & String.Format("{0:f}", FBOCUP_R) & ";" & String.Format("{0:f}", FBOCUP_L) & vbCrLf
    End Function
    Public Sub SetFBSGIN(ByVal Right As String, ByVal Left As String)
        FBSGIN_R = Right
        FBSGIN_L = Left
    End Sub
    Private Function GetFBSGIN() As String
        Return "FBSGIN=" & String.Format("{0:f}", FBSGIN_R) & ";" & String.Format("{0:f}", FBSGIN_L) & vbCrLf
    End Function
    Public Sub SetFBSGUP(ByVal Right As String, ByVal Left As String)
        FBSGUP_R = Right
        FBSGUP_L = Left
    End Sub
    Private Function GetFBSGUP() As String
        Return "FBSGUP=" & String.Format("{0:f}", FBSGUP_R) & ";" & String.Format("{0:f}", FBSGUP_L) & vbCrLf
    End Function
    Public Sub SetFCOCIN(ByVal Right As String, ByVal Left As String)
        FCOCIN_R = Right
        FCOCIN_L = Left
    End Sub
    Public Sub SetFCOCIN(ByVal value As String)
        Dim temp As String = value.Substring(value.IndexOf("=") + 1, value.IndexOf(";") - value.IndexOf("=") - 1)
        FCOCIN_R = temp
        FCOCIN_L = value.Substring(value.IndexOf(";") + 1)
    End Sub
    Public Function GetFCOCIN() As String
        Return "FCOCIN=" & String.Format("{0:f}", FCOCIN_R) & ";" & String.Format("{0:f}", FCOCIN_L) & vbCrLf
    End Function
    Public Sub SetFCOCUP(ByVal Right As String, ByVal Left As String)
        FCOCUP_R = Right
        FCOCUP_L = Left
    End Sub
    Public Sub SetFCOCUP(ByVal value As String)
        Dim temp As String = value.Substring(value.IndexOf("=") + 1, value.IndexOf(";") - value.IndexOf("=") - 1)
        FCOCUP_R = temp
        FCOCUP_L = value.Substring(value.IndexOf(";") + 1)
    End Sub
    Public Function GetFCOCUP() As String
        Return "FCOCUP=" & String.Format("{0:f}", FCOCUP_R) & ";" & String.Format("{0:f}", FCOCUP_L) & vbCrLf
    End Function
    Public Sub SetFCSGIN(ByVal Right As String, ByVal Left As String)
        FCSGIN_R = Right
        FCSGIN_L = Left
    End Sub
    Public Sub SetFCSGIN(ByVal value As String)
        Dim temp As String = value.Substring(value.IndexOf("=") + 1, value.IndexOf(";") - value.IndexOf("=") - 1)
        FCSGIN_R = temp
        FCSGIN_L = value.Substring(value.IndexOf(";") + 1)
    End Sub
    Public Function GetFCSGIN() As String
        Return "FCSGIN=" & String.Format("{0:f}", FCSGIN_R) & ";" & String.Format("{0:f}", FCSGIN_L) & vbCrLf
    End Function
    Public Sub SetFCSGUP(ByVal Right As String, ByVal Left As String)
        FCSGUP_R = Right
        FCSGUP_L = Left
    End Sub
    Public Sub SetFCSGUP(ByVal value As String)
        Dim temp As String = value.Substring(value.IndexOf("=") + 1, value.IndexOf(";") - value.IndexOf("=") - 1)
        FCSGUP_R = temp
        FCSGUP_L = value.Substring(value.IndexOf(";") + 1)
    End Sub
    Public Function GetFCSGUP() As String
        Return "FCSGUP=" & String.Format("{0:f}", FCSGUP_R) & ";" & String.Format("{0:f}", FCSGUP_L) & vbCrLf
    End Function
    Public Sub SetSGOCIN(ByVal Right As String, ByVal Left As String)
        SGOCIN_R = Right
        SGOCIN_L = Left
    End Sub
    Private Function GetSGOCIN() As String
        Return "SGOCIN=" & String.Format("{0:f}", SGOCIN_R) & ";" & String.Format("{0:f}", SGOCIN_L) & vbCrLf
    End Function
    Public Sub SetSGOCUP(ByVal Right As String, ByVal Left As String)
        SGOCUP_R = Right
        SGOCUP_L = Left
    End Sub
    Private Function GetSGOCUP() As String
        Return "SGOCUP=" & String.Format("{0:f}", SGOCUP_R) & ";" & String.Format("{0:f}", SGOCUP_L) & vbCrLf
    End Function
    Public Sub SetCTHICK(ByVal Right As String, ByVal Left As String)
        CTHICK_R = Right
        CTHICK_L = Left
    End Sub
    Public Function GetCTHICK() As String
        Return "CTHICK=" & String.Format("{0:f}", CTHICK_R) & ";" & String.Format("{0:f}", CTHICK_L) & vbCrLf
    End Function
    Public Sub SetTHKA(ByVal Right As String, ByVal Left As String)
        THKA_R = Right
        THKA_L = Left
    End Sub
    Private Function GetTHKA() As String
        Return "THKA=" & String.Format("{0:f}", THKA_R) & ";" & String.Format("{0:f}", THKA_L) & vbCrLf
    End Function
    Public Sub SetTHKP(ByVal Right As Single, ByVal Left As Single)
        THKP_R = Right
        THKP_L = Left
    End Sub
    Public Function GetTHKP() As String
        Return "THKP=" & String.Format("{0:f}", THKP_R) & ";" & String.Format("{0:f}", THKP_L) & vbCrLf
    End Function
    Public Sub SetTHNA(ByVal Right As Single, ByVal Left As Single)
        THNA_R = Right
        THNA_L = Left
    End Sub
    Private Function GetTHNA() As String
        Return "THNA=" & String.Format("{0:f}", THNA_R) & ";" & String.Format("{0:f}", THNA_L) & vbCrLf
    End Function
    Public Sub SetTHNP(ByVal Right As Single, ByVal Left As Single)
        THNP_R = Right
        THNP_L = Left
    End Sub
    Private Function GetTHNP() As String
        Return "THNP=" & String.Format("{0:f}", THNP_R) & ";" & String.Format("{0:f}", THNP_L) & vbCrLf
    End Function
    Public Sub SetMCIRC(ByVal MCIRC As Single)
        Me.MCIRC = MCIRC
    End Sub
    Private Function GetMCIRC() As String
        Return "MCIRC=" & String.Format("{0:f}", THNP_R) & vbCrLf
    End Function
    Public Sub SetSLBP(ByVal Right As Single, ByVal Left As Single)
        SLBP_R = Right
        SLBP_L = Left
    End Sub
    Private Function GetSLBP() As String
        Return "SLBP=" & String.Format("{0:f}", SLBP_R) & ";" & String.Format("{0:f}", SLBP_L) & vbCrLf
    End Function
    Public Sub SetTRCFMT_R(ByVal value As String)
        If value.Contains(vbCrLf) Then
            value = value.Replace(vbCrLf, " ")
        End If
        If value.Contains(vbCr) Then
            value = value.Replace(vbCr, vbCrLf)
        End If

        If value.EndsWith(" ") Then
            value = value.Substring(0, value.Length - 1)
        End If
        TRCFMT_R = value.Replace(" ", vbCrLf)
        TRCFMT_R = TRCFMT_R.Replace(vbCrLf & vbCrLf, vbCrLf)
    End Sub
    Public Sub SetTRCFMT_L(ByVal value As String)
        If value.Contains(vbCrLf) Then
            value = value.Replace(vbCrLf, " ")
        End If
        If value.Contains(vbCr) Then
            value = value.Replace(vbCr, vbCrLf)
        End If

        If value.EndsWith(" ") Then
            value = value.Substring(0, value.Length - 1)
        End If
        TRCFMT_L = value.Replace(" ", vbCrLf)
        TRCFMT_L = TRCFMT_L.Replace(vbCrLf & vbCrLf, vbCrLf)
    End Sub
    Public Sub SetTRCFMT(ByVal Format As TRACEDataFormatIdentifier, ByVal RadiusQty As Integer, ByVal RadiusMode As TRACERadiusModeIdentifier, ByVal Side As TRACESide, ByVal TracedObject As TRACEObject, ByVal Radius() As Integer)
        Dim Value As String = ""
        Dim Temp As String = "000"

        '----------------------------------------------------------------------
        ' Aqui agrego el Formato
        '----------------------------------------------------------------------
        Value = "TRCFMT=" & Format & ";"
        '----------------------------------------------------------------------
        ' Aqui agrego la cantidad de trazos rellenados con ceros (0)
        '----------------------------------------------------------------------
        Temp = (Temp & RadiusQty)
        Temp = Temp.Substring(Temp.Length - 3)
        Value &= Temp & ";"
        '----------------------------------------------------------------------
        ' Aqui le agrego el RadiusMode
        '----------------------------------------------------------------------
        Select Case RadiusMode
            Case TRACERadiusModeIdentifier.EvenlySpaced
                Value &= "E;"
            Case TRACERadiusModeIdentifier.UnevenlySpaced
                Value &= "U;"
        End Select
        '----------------------------------------------------------------------
        ' Aqui le agrego el Lado del trazo
        '----------------------------------------------------------------------
        Select Case Side
            Case TRACESide.Both
                Value &= "B;"
            Case TRACESide.Left
                Value &= "L;"
            Case TRACESide.Right
                Value &= "R;"
        End Select
        '----------------------------------------------------------------------
        ' Aqui le agrego que fue lo que se trazó
        '----------------------------------------------------------------------
        Select Case TracedObject
            Case TRACEObject.Demo
                Value &= "D"
            Case TRACEObject.Frame
                Value &= "F"
            Case TRACEObject.Packet
                Value &= "P"
        End Select
        Select Case Side
            Case TRACESide.Left
                TRCFMT_L = Value
                RADIUS_L = Radius
            Case TRACESide.Right
                TRCFMT_R = Value
                RADIUS_R = Radius
            Case TRACESide.Both
                TRCFMT_L = Value
                TRCFMT_R = Value
                RADIUS_L = Radius
                RADIUS_R = Radius
        End Select
    End Sub
    Private Function GetTRCFMT(ByVal Side As TRACESide) As String
        Dim Value As String = ""
        Dim Temp As String
        Dim ThisRadius() As Integer = {0}
        Dim i, count As Integer

        Dim Filled As Boolean = False

        Select Case Side
            Case TRACESide.Right
                Value = TRCFMT_R & vbCrLf
                ThisRadius = RADIUS_R
                If TRCFMT_R.Contains(vbCrLf) Then
                    Filled = True
                End If
            Case TRACESide.Left
                Value = TRCFMT_L & vbCrLf
                ThisRadius = RADIUS_L
                If TRCFMT_R.Contains(vbCrLf) Then
                    Filled = True
                End If
            Case TRACESide.Both
                Value = TRCFMT_R & vbCrLf
                ThisRadius = RADIUS_R
                If TRCFMT_R.Contains(vbCrLf) Then
                    Filled = True
                End If
        End Select

        If Not Filled Then
            count = 0
            Temp = ""
            For i = 0 To ThisRadius.Length - 1
                If count = 0 Then
                    Temp = "R=" & Temp & ThisRadius(i)
                    count += 1
                ElseIf count < 10 Then
                    If Temp.Length > 2 Then
                        Temp &= ";"
                    End If
                    Temp &= ThisRadius(i)
                    count += 1
                Else
                    Value &= Temp & vbCrLf
                    count = 0
                    i -= 1
                    Temp = ""
                End If
            Next
            If Temp.Length > 0 Then
                Value &= Temp & vbCrLf
            End If
        End If

        Return Value
    End Function
    Public Sub SetZFMT(ByVal Format As TRACEDataFormatIdentifier, ByVal RadiusQty As Integer, ByVal RadiusMode As TRACERadiusModeIdentifier, ByVal Side As TRACESide, ByVal TracedObject As TRACEObject, ByVal Z_Radius() As Integer)
        Dim Value As String = ""
        Dim Temp As String = "000"

        '----------------------------------------------------------------------
        ' Aqui agrego el Formato
        '----------------------------------------------------------------------
        Value = "ZFMT=" & Format & ";"
        '----------------------------------------------------------------------
        ' Aqui agrego la cantidad de trazos rellenados con ceros (0)
        '----------------------------------------------------------------------
        Temp = (Temp & RadiusQty)
        Temp = Temp.Substring(Temp.Length - 3)
        Value &= Temp & ";"
        '----------------------------------------------------------------------
        ' Aqui le agrego el RadiusMode
        '----------------------------------------------------------------------
        Select Case RadiusMode
            Case TRACERadiusModeIdentifier.EvenlySpaced
                Value &= "E;"
            Case TRACERadiusModeIdentifier.UnevenlySpaced
                Value &= "U;"
        End Select
        '----------------------------------------------------------------------
        ' Aqui le agrego el Lado del trazo
        '----------------------------------------------------------------------
        Select Case Side
            Case TRACESide.Both
                Value &= "B;"
            Case TRACESide.Left
                Value &= "L;"
            Case TRACESide.Right
                Value &= "R;"
        End Select
        '----------------------------------------------------------------------
        ' Aqui le agrego que fue lo que se trazó
        '----------------------------------------------------------------------
        Select Case TracedObject
            Case TRACEObject.Demo
                Value &= "D"
            Case TRACEObject.Frame
                Value &= "F"
            Case TRACEObject.Packet
                Value &= "P"
        End Select
        Select Case Side
            Case TRACESide.Left
                ZFMT_L = Value
                ZRADIUS_L = Z_Radius
            Case TRACESide.Right
                ZFMT_R = Value
                ZRADIUS_R = Z_Radius
            Case TRACESide.Both
                ZFMT_L = Value
                ZFMT_R = Value
                ZRADIUS_L = Z_Radius
                ZRADIUS_R = Z_Radius
        End Select
    End Sub
    Private Function GetZFMT(ByVal Side As TRACESide) As String
        Dim Value As String = ""
        Dim Temp As String
        Dim ThisRadius() As Integer = {0}
        Dim i, count As Integer

        Select Case Side
            Case TRACESide.Right
                Value = ZFMT_R & vbCrLf
                ThisRadius = ZRADIUS_R
            Case TRACESide.Left
                Value = ZFMT_L & vbCrLf
                ThisRadius = ZRADIUS_L
            Case TRACESide.Both
                Value = ZFMT_R & vbCrLf
                ThisRadius = ZRADIUS_R
        End Select

        count = 0
        Temp = ""
        For i = 0 To ThisRadius.Length - 1
            If count = 0 Then
                Temp = "R=" & Temp & ThisRadius(i)
                count += 1
            ElseIf count < 19 Then
                If Temp.Length > 2 Then
                    Temp &= ";"
                End If
                Temp &= ThisRadius(i)
                count += 1
            Else
                Value &= Temp & vbCrLf
                count = 0
                i -= 1
                Temp = ""
            End If
        Next
        If Temp.Length > 0 Then
            Value &= Temp & vbCrLf
        End If

        Return Value
    End Function
    Public Sub SetDO(ByVal DO_Command As EyeSides)
        Select Case DO_Command
            Case EyeSides.Both
                Me.DO_Command = "B"
            Case EyeSides.Left
                Me.DO_Command = "L"
            Case EyeSides.Right
                Me.DO_Command = "R"
        End Select
    End Sub
    Private Function GetDO() As String
        Return "DO=" & DO_Command & vbCrLf
    End Function
    'Public Sub SetETYP(ByVal Options As LensTypes)
    '    ETYP = Options
    'End Sub
    Public Sub SetETYP(ByVal Right As LensTypes, ByVal Left As LensTypes)
        ETYP_R = Right
        ETYP_L = Left
    End Sub
    Public Function GetETYP() As String
        Return "ETYP=" & ETYP_R & ";" & ETYP_L & vbCrLf
        'Return "ETYP=" & ETYP & vbCrLf
    End Function
    Public Sub SetFTYP(ByVal Options As FrameTypes)
        FTYP = Options
    End Sub
    Public Function GetFTYP() As String
        Return "FTYP=" & FTYP & vbCrLf
    End Function
    Public Sub SetFPINB(ByVal Right As Single, ByVal Left As Single)
        FPINB_R = Right
        FPINB_L = Left
    End Sub
    Public Function GetFPINB() As String
        Return "FPINB=" & String.Format("{0:f}", FPINB_R) & ";" & String.Format("{0:f}", FPINB_L) & vbCrLf
    End Function
    Public Sub SetPINB(ByVal Right As Single, ByVal Left As Single)
        PINB_R = Right
        PINB_L = Left
    End Sub
    Public Function GetPINB() As String
        Return "PINB=" & String.Format("{0:f}", PINB_R) & ";" & String.Format("{0:f}", PINB_L) & vbCrLf
    End Function
    Public Sub SetGDEPTH(ByVal Right As Single, ByVal Left As Single)
        GDEPTH_R = Right
        GDEPTH_L = Left
    End Sub
    Public Function GetGDEPTH() As String
        Return "GDEPTH=" & String.Format("{0:f}", GDEPTH_R) & ";" & String.Format("{0:f}", GDEPTH_L) & vbCrLf
    End Function
    Public Sub SetGWIDTH(ByVal Right As Single, ByVal Left As Single)
        GWIDTH_R = Right
        GWIDTH_L = Left
    End Sub
    Public Function GetGWIDTH() As String
        Return "GWIDTH=" & String.Format("{0:f}", GWIDTH_R) & ";" & String.Format("{0:f}", GWIDTH_L) & vbCrLf
    End Function
    Public Sub SetBEVP(ByVal Right As BevelPossition, ByVal Left As BevelPossition)
        BEVP_R = Right
        BEVP_L = Left
    End Sub
    Public Sub SetBEVP(ByVal Right As String, ByVal Left As String)
        BEVP_R = Right
        BEVP_L = Left
    End Sub
    Public Function GetBEVP() As String
        Return "BEVP=" & BEVP_R & ";" & BEVP_L & vbCrLf
    End Function
    Public Sub SetBEVC(ByVal Right As String, ByVal Left As String)
        BEVC_R = Right
        BEVC_L = Left
    End Sub
    Public Sub SetBEVC(ByVal Right As Single, ByVal Left As Single)
        BEVC_R = Right
        BEVC_L = Left
    End Sub
    Private Function GetBEVC() As String
        Return "BEVC=" & String.Format("{0:0.000}", BEVC_R) & ";" & String.Format("{0:0.000}", BEVC_L) & vbCrLf
    End Function
    Public Sub SetPOLISH(ByVal Options As PolishingOptions)
        POLISH = Options
    End Sub
    Public Function GetPOLISH() As String
        Return "POLISH=" & POLISH & vbCrLf
    End Function
    Public Sub SetCLAMP(ByVal Right As Integer, ByVal Left As Integer)
        CLAMP_R = Right
        CLAMP_L = Left
    End Sub
    Private Function GetCLAMP() As String
        Return "CLAMP=" & CLAMP_R & ";" & CLAMP_L & vbCrLf
    End Function
    Public Sub SetDRILL(ByVal value As String)
        If value.StartsWith("DRILL=") Then
            value = value.Replace("DRILL=", "")
        End If
        DRILL = value
    End Sub
    Public Sub SetDRILL(ByVal Sides As EyeSides, ByVal x_start As Single, ByVal y_start As Single, ByVal diameter As Single, ByVal x_end As Single, ByVal y_end As Single, ByVal depth As Single, ByVal DrillAngle As DrillAngleType, ByVal lateral_angle As Integer, ByVal vertical_angle As Integer)
        Dim value As String = ""
        'value = "DRILL="
        '----------------------------------------------------------------------
        ' Aqui le agrego el Lado
        '----------------------------------------------------------------------
        Select Case Sides
            Case EyeSides.Both
                value &= "B;"
            Case EyeSides.Left
                value &= "L;"
            Case EyeSides.Right
                value &= "R;"
        End Select
        '----------------------------------------------------------------------
        ' Aqui le agrego el punto de inicio (x,y)
        '----------------------------------------------------------------------
        value &= String.Format("{0:f}", x_start) & ";" & String.Format("{0:f}", y_start) & ";"
        '----------------------------------------------------------------------
        ' Aqui le agrego el diametro
        '----------------------------------------------------------------------
        value &= String.Format("{0:f}", diameter) & ";"
        '----------------------------------------------------------------------
        ' Aqui le agrego el punto final (x,y)
        '----------------------------------------------------------------------
        value &= String.Format("{0:f}", x_end) & ";" & String.Format("{0:f}", y_end) & ";"
        '----------------------------------------------------------------------
        ' Aqui le agrego el DEPTH
        '----------------------------------------------------------------------
        value &= String.Format("{0:f}", depth) & ";"
        '----------------------------------------------------------------------
        ' Aqui le agrego el Angulo del Drill
        '----------------------------------------------------------------------
        Select Case DrillAngle
            Case DrillAngleType.AngleSpecified
                value &= "A;"
            Case DrillAngleType.NormalToBack
                value &= "B;"
            Case DrillAngleType.NormalToFront
                value &= "F;"
        End Select
        '----------------------------------------------------------------------
        ' Aqui le agrego el angulo lateral y vertical
        '----------------------------------------------------------------------
        value &= String.Format("{0:f}", lateral_angle) & ";" & String.Format("{0:f}", vertical_angle)
        DRILL = value
    End Sub
    Public Function GetDRILL() As String
        Return "DRILL=" & DRILL & vbCrLf
    End Function
    Public Function GetRxNum() As String
        Return RXNM
    End Function
    Public Function GetAccountNum() As String
        Return ACCN
    End Function
    Public Sub SetDRILLE(ByVal Sides As EyeSides, ByVal locandref As DrillLocationAndReference, ByVal nasortemp As DrillNasalOrTemporalSide, ByVal mounting As DrillMounting, ByVal x_start As Single, ByVal y_start As Single, ByVal diameter As Single, ByVal x_end As Single, ByVal y_end As Single, ByVal depth As Single, ByVal featuretype As DrillFeatureType, ByVal DrillAngle As DrillAngleType, ByVal lateral_angle As Integer, ByVal vertical_angle As Integer)
        Dim value As String = ""
        'value = "DRILLE="
        '----------------------------------------------------------------------
        ' Aqui le agrego el Lado
        '----------------------------------------------------------------------
        Select Case Sides
            Case EyeSides.Both
                value &= "B;"
            Case EyeSides.Left
                value &= "L;"
            Case EyeSides.Right
                value &= "R;"
            Case EyeSides.None          ' Si no tiene ojo seleccionado, se manda solo el somando "DRILLE=0"
                value &= "0"
                DRILLE = value
                Exit Sub
        End Select
        '----------------------------------------------------------------------
        ' Aqui le agrego location and reference
        '----------------------------------------------------------------------
        Select Case locandref
            Case DrillLocationAndReference.CenterReference
                value &= "C"
            Case DrillLocationAndReference.EdgeReference
                value &= "E"
            Case DrillLocationAndReference.BoxReference
                value &= "B"
        End Select
        If locandref = DrillLocationAndReference.BoxReference Or locandref = DrillLocationAndReference.EdgeReference Then
            Select Case nasortemp
                Case DrillNasalOrTemporalSide.Nasal
                    value &= "N"
                Case DrillNasalOrTemporalSide.Temporal
                    value &= "T"
            End Select
        End If
        Select Case mounting
            Case DrillMounting.Front
                value &= "F"
            Case DrillMounting.Rear
                value &= "R"
        End Select
        value &= ";"
        '----------------------------------------------------------------------
        ' Aqui le agrego el punto de inicio (x,y)
        '----------------------------------------------------------------------
        value &= String.Format("{0:f}", x_start) & ";" & String.Format("{0:f}", y_start) & ";"
        '----------------------------------------------------------------------
        ' Aqui le agrego el diametro
        '----------------------------------------------------------------------
        value &= String.Format("{0:f}", diameter) & ";"
        '----------------------------------------------------------------------
        ' Aqui le agrego el punto final (x,y)
        '----------------------------------------------------------------------
        value &= String.Format("{0:f}", x_end) & ";" & String.Format("{0:f}", y_end) & ";"
        '----------------------------------------------------------------------
        ' Aqui le agrego el DEPTH
        '----------------------------------------------------------------------
        value &= String.Format("{0:f}", depth) & ";"
        '----------------------------------------------------------------------
        ' Aqui le agrego el Feature Type
        '----------------------------------------------------------------------
        value &= featuretype & ";"
        '----------------------------------------------------------------------
        ' Aqui le agrego el Angulo del Drill
        '----------------------------------------------------------------------
        Select Case DrillAngle
            Case DrillAngleType.AngleSpecified
                value &= "A;"
            Case DrillAngleType.NormalToBack
                value &= "B;"
            Case DrillAngleType.NormalToFront
                value &= "F;"
        End Select
        '----------------------------------------------------------------------
        ' Aqui le agrego el angulo lateral y vertical
        '----------------------------------------------------------------------
        value &= String.Format("{0:f}", lateral_angle) & ";" & String.Format("{0:f}", vertical_angle)
        DRILLE = value
    End Sub
    Public Sub SetDRILLE(ByVal Sides As EyeSides, ByVal locandref As DrillLocationAndReference, ByVal nasortemp As DrillNasalOrTemporalSide, ByVal mounting As DrillMounting, ByVal x As Single, ByVal y As Single)
        Dim value As String = ""
        'value = "DRILLE="
        '----------------------------------------------------------------------
        ' Aqui le agrego el Lado
        '----------------------------------------------------------------------
        Select Case Sides
            Case EyeSides.Both
                value &= "B;"
            Case EyeSides.Left
                value &= "L;"
            Case EyeSides.Right
                value &= "R;"
            Case EyeSides.None          ' Si no tiene ojo seleccionado, se manda solo el somando "DRILLE=0"
                value &= "0"
                DRILLE = value
                Exit Sub
        End Select
        '----------------------------------------------------------------------
        ' Aqui le agrego location and reference
        '----------------------------------------------------------------------
        Select Case locandref
            Case DrillLocationAndReference.CenterReference
                value &= "C"
            Case DrillLocationAndReference.EdgeReference
                value &= "E"
            Case DrillLocationAndReference.BoxReference
                value &= "B"
        End Select
        If locandref = DrillLocationAndReference.BoxReference Or locandref = DrillLocationAndReference.EdgeReference Then
            Select Case nasortemp
                Case DrillNasalOrTemporalSide.Nasal
                    value &= "N"
                Case DrillNasalOrTemporalSide.Temporal
                    value &= "T"
            End Select
        End If
        Select Case mounting
            Case DrillMounting.Front
                value &= "F"
            Case DrillMounting.Rear
                value &= "R"
        End Select
        value &= ";"
        '----------------------------------------------------------------------
        ' Aqui le agrego el punto de inicio (x,y)
        '----------------------------------------------------------------------
        value &= String.Format("{0:f}", x) & ";" & String.Format("{0:f}", y) & ";"
        DRILLE = value
    End Sub
    Private Function GetDRILLE() As String
        Return "DRILLE=" & DRILLE & vbCrLf
    End Function
    Public Sub SetANS(ByVal Value As Integer)
        ANS = Value
    End Sub
    Private Function GetANS() As String
        Return "ANS=" & ANS & vbCrLf
    End Function
    Public Sub SetJOB(ByVal Value As String)
        JOB = Value
    End Sub
    Private Function GetJOB() As String
        Return "JOB=" & JOB & vbCrLf
    End Function
    Public Function GetFileName() As String
        Return JOB & ".dat"
    End Function
    Public Sub SetSTATUS(ByVal Value As String)
        STATUS = Value
    End Sub
    Private Function GetSTATUS() As String
        Return "STATUS=" & STATUS & vbCrLf
    End Function
    Public Sub Set_ETYP2(ByVal Right As Integer, ByVal Left As Integer)
        _ETYP2_R = Right
        _ETYP2_L = Left
    End Sub
    Public Function Get_ETYP2() As String
        Return "_ETYP2=" & _ETYP2_R & ";" & _ETYP2_L & vbCrLf
    End Function
    Public Sub SetBEVM(ByVal Right As Single, ByVal Left As Single)
        BEVM_R = Right
        BEVM_L = Left
    End Sub
    Public Function GetBEVM() As String
        Return "BEVM=" & String.Format("{0:f}", BEVM_R) & ";" & String.Format("{0:f}", BEVM_L) & vbCrLf
    End Function
    Public Sub Set_CKBEV(ByVal Right As Integer, ByVal Left As Integer)
        _CKBEV_R = Right
        _CKBEV_L = Left
    End Sub
    Public Function Get_CKBEV() As String
        Return "_CKBEV=" & _CKBEV_R & ";" & _CKBEV_L & vbCrLf
    End Function
    Public Sub Set_FRNT(ByVal Right As Single, ByVal Left As Single)
        _FRNT_R = Right
        _FRNT_L = Left
    End Sub
    Private Function Get_FRNT() As String
        Return "_FRNT=" & String.Format("{0:f}", _FRNT_R) & ";" & String.Format("{0:f}", _FRNT_L) & vbCrLf
    End Function
    Public Sub Set_POLISH2(ByVal Right As Integer, ByVal Left As Integer)
        _POLISH2_R = Right
        _POLISH2_L = Left
    End Sub
    Private Function Get_POLISH2() As String
        Return "_POLISH2=" & _POLISH2_R & ";" & _POLISH2_L & vbCrLf
    End Function
    Public Sub SetBSIZ(ByVal Right As Single, ByVal Left As Single)
        BSIZ_R = Right
        BSIZ_L = Left
    End Sub
    Public Function GetBSIZ() As String
        Return "BSIZ=" & String.Format("{0:f}", BSIZ_R) & ";" & String.Format("{0:f}", BSIZ_L) & vbCrLf
    End Function

    Public Sub Set_PINONOF(ByVal Right As Integer, ByVal Left As Integer)
        _PINONOF_R = Right
        _PINONOF_L = Left
    End Sub
    Public Function Get_PINONOF() As String
        Return "_PINONOF=" & _PINONOF_R & ";" & _PINONOF_L & vbCrLf
    End Function
    Public Sub Set_FPINONF(ByVal Right As Integer, ByVal Left As Integer)
        _FPINONF_R = Right
        _FPINONF_L = Left
    End Sub
    Public Function Get_FPINONF() As String
        Return "_FPINONF=" & _FPINONF_R & ";" & _FPINONF_L & vbCrLf
    End Function
    Public Sub Set_IDSEQ(ByVal Right As Integer, ByVal Left As Integer)
        _IDSEQ_R = Right
        _IDSEQ_L = Left
    End Sub
    Private Function Get_IDSEQ() As String
        Return "_IDSEQ=" & _IDSEQ_R & ";" & _IDSEQ_L & vbCrLf
    End Function
    Public Sub SetFCRV(ByVal Right As Single, ByVal Left As Single)
        FCRV_R = Right
        FCRV_L = Left
    End Sub
    Public Sub SetFCRV(ByVal Right As String, ByVal Left As String)
        FCRV_R = Right
        FCRV_L = Left
    End Sub
    Public Function GetFCRV() As String
        Return "FCRV=" & String.Format("{0:f}", FCRV_R) & ";" & String.Format("{0:f}", FCRV_L) & vbCrLf
    End Function
    Public Sub Set_VARINC(ByVal Right As Integer, ByVal Left As Integer)
        _VARINC_R = Right
        _VARINC_L = Left
    End Sub
    Public Function Get_VARINC() As String
        Return "_VARINC=" & _VARINC_R & ";" & _VARINC_L & vbCrLf
    End Function
    Public Sub Set_VANGLE(ByVal Right As Integer, ByVal Left As Integer)
        _VANGLE_R = Right
        _VANGLE_L = Left
    End Sub
    Public Function Get_VANGLE() As String
        Return "_VANGLE=" & _VANGLE_R & ";" & _VANGLE_L & vbCrLf
    End Function
    Public Sub Set_LCOAT(ByVal Right As CoatingSlipperiness, ByVal Left As CoatingSlipperiness)
        _VANGLE_R = Right
        _VANGLE_L = Left
    End Sub
    Public Function Get_LCOAT() As String
        Return "_LCOAT=" & _LCOAT_R & ";" & _LCOAT_L & vbCrLf
    End Function
    Public Sub Set_AUTOBFR(ByVal Right As CurvePriority, ByVal Left As CurvePriority)
        _AUTOBFR_R = Right
        _AUTOBFR_L = Left
    End Sub
    Public Function Get_AUTOBFR() As String
        Return "_AUTOBFR=" & _AUTOBFR_R & ";" & _AUTOBFR_L & vbCrLf
    End Function
    Public Sub Set_CKDOCUR(ByVal value As EnableStatus)
        _CKDOCUR = value
    End Sub
    Private Function Get_CKDOCUR() As String
        Return "_CKDOCUR=" & _CKDOCUR & vbCrLf
    End Function
    Public Sub Set_NDOCUR(ByVal value As EnableStatus)
        _NDOCUR = value
    End Sub
    Private Function Get_NDOCUR() As String
        Return "_NDOCUR=" & _NDOCUR & vbCrLf
    End Function
    Public Sub Set_CURVELV(ByVal value As Integer)
        _CURVELV = value
    End Sub
    Private Function Get_CURVELV() As String
        Return "_CURVELV=" & _CURVELV & vbCrLf
    End Function
    Public Sub Set_LPRESS(ByVal Right As Integer, ByVal Left As Integer)
        _LPRESS_R = right
        _LPRESS_L = left
    End Sub
    Private Function Get_LPRESS() As String
        Return "_LPRESS=" & _LPRESS_R & ";" & _LPRESS_L & vbCrLf
    End Function
    Public Sub Set_CIRCON(ByVal Right As Integer, ByVal Left As Integer)
        _CIRCON_R = right
        _CIRCON_L = left
    End Sub
    Public Function Get_CIRCON() As String
        Return "_CIRCON=" & _CIRCON_R & ";" & _CIRCON_L & vbCrLf
    End Function
    Public Sub Set_FBFCANG(ByVal Right As Integer, ByVal Left As Integer)
        _FBFCANG_R = Right
        _FBFCANG_L = Left
    End Sub
    Private Function Get_FBFCANG() As String
        Return "_FBFCANG=" & _FBFCANG_R & ";" & _FBFCANG_L & vbCrLf
    End Function
    Public Sub SetFRAM(ByVal value As String)
        FRAM = value
    End Sub
    Private Function GetFRAM() As String
        Return "FRAM=" & FRAM & vbCrLf
    End Function
    Public Sub Set_DRILLREF(ByVal Side As EyeSides, ByVal Ref As Integer, ByVal x As Single, ByVal y As Single, ByVal RefShown As Integer)
        Dim value As String = ""
        ' value = "_DRILLREF="
        'Aqui le agrego el lado
        Select Case Side
            Case EyeSides.Both
                value &= "B;"
            Case EyeSides.Left
                value &= "L;"
            Case EyeSides.Right
                value &= "R"
        End Select
        'Aqui le agrego la referencia
        value &= Ref & ";"
        'Aqui le agrego el punto
        value &= x & ";" & y & ";"
        'Aqui le agrego si la referencia se vera en la interface
        value &= RefShown
    End Sub
    Private Function Get_DRILLREF() As String
        Return "_DRILLREF=" & _DRILLREF & vbCrLf
    End Function
    Public Sub Set_DRILLLNK()

    End Sub
    Private Function Get_DRILLLNK() As String
        Return "_DRILLLNK=" & _DRILLLNK & vbCrLf
    End Function
    Public Sub Set_VVINCON(ByVal right As String, ByVal left As String)
        _VVINCON_L = left
        _VVINCON_R = right
    End Sub
    Public Function Get_VVINCON() As String
        Return "_VVINCON=" & _VVINCON_R & ";" & _VVINCON_L & vbCrLf
    End Function
    Public Sub Set_VANGLON(ByVal right As String, ByVal left As String)
        _VANGLON_L = left
        _VANGLON_R = right
    End Sub
    Public Function Get_VANGLON() As String
        Return "_VANGLON=" & _VANGLON_R & ";" & _VANGLON_L & vbCrLf
    End Function
    Public Sub Set_CHKRAD(ByVal right As String, ByVal left As String)
        _CHKRAD_R = right
        _CHKRAD_L = left
    End Sub
    Public Function Get_CHKRAD() As String
        Return "_CHKRAD=" & _CHKRAD_R & ";" & _CHKRAD_L & vbCrLf
    End Function
    Public Sub Set_TBASE(ByVal right As String, ByVal left As String)
        _TBASE_R = right
        _TBASE_L = left
    End Sub
    Public Function Get_TBASE() As String
        Return "_TBASE=" & _TBASE_R & ";" & _TBASE_L & vbCrLf
    End Function
    Public Sub Set_CHRFRAM(ByVal value As String)
        _CHRFRAM = value
    End Sub
    Public Function Get_CHRFRAM() As String
        Return "_CHRFRAM=" & _CHRFRAM & vbCrLf
    End Function
    Public Sub Set_TBASEFR(ByVal value As String)
        _TBASEFR = value
    End Sub
    Public Function Get_TBASEFR() As String
        Return "_TBASEFR=" & _TBASEFR & vbCrLf
    End Function
    Public Sub Set_CKSEQMAN(ByVal value As String)
        _CKSEQMAN = value
    End Sub
    Public Function Get_CKSEQMAN() As String
        Return "_CKSEQMAN=" & _CKSEQMAN & vbCrLf
    End Function
    Public Sub Set_COPYDXSX(ByVal value As String)
        _COPYDXSX = value
    End Sub
    Public Function Get_COPYDXSX() As String
        Return "_COPYDXSX=" & _COPYDXSX & vbCrLf
    End Function
    Public Sub Set_VDEPTON(ByVal right As String, ByVal left As String)
        _VDEPTON_R = right
        _VDEPTON_L = left
    End Sub
    Public Function Get_VDEPTON() As String
        Return "_VDEPTON=" & _VDEPTON_R & ";" & _VDEPTON_L & vbCrLf
    End Function

    Public Sub Set_GANGS(ByVal right As String, ByVal left As String)
        _GANGS_R = right
        _GANGS_L = left
    End Sub
    Public Function Get_GANGS() As String
        Return "_GANGS=" & _GANGS_R & ";" & _GANGS_L & vbCrLf
    End Function

    Public Sub Set_GANGE(ByVal right As String, ByVal left As String)
        _GANGE_R = right
        _GANGE_L = left
    End Sub
    Public Function Get_GANGE() As String
        Return "_GANGE=" & _GANGE_R & ";" & _GANGE_L & vbCrLf
    End Function

    Public Sub Set_GDEPTH2(ByVal right As String, ByVal left As String)
        _GDEPTH2_R = right
        _GDEPTH2_L = left
    End Sub
    Public Function Get_GDEPTH2() As String
        Return "_GDEPTH2=" & _GDEPTH2_R & ";" & _GDEPTH2_L & vbCrLf
    End Function
    Public Sub Set_GWIDTH2(ByVal right As String, ByVal left As String)
        _GWIDTH2_R = right
        _GWIDTH2_L = left
    End Sub
    Public Function Get_GWIDTH2() As String
        Return "_GWIDTH2=" & _GWIDTH2_R & ";" & _GWIDTH2_L & vbCrLf
    End Function
    Public Sub Set_GANGS2(ByVal right As String, ByVal left As String)
        _GANGS2_R = right
        _GANGS2_L = left
    End Sub
    Public Function Get_GANGS2() As String
        Return "_GANGS2=" & _GANGS2_R & ";" & _GANGS2_L & vbCrLf
    End Function

    Public Sub Set_GANGE2(ByVal right As String, ByVal left As String)
        _GANGE2_R = right
        _GANGE2_L = left
    End Sub
    Public Function Get_GANGE2() As String
        Return "_GANGE2=" & _GANGE2_R & ";" & _GANGE2_L & vbCrLf
    End Function
    Public Sub Set_GRV2ON(ByVal right As String, ByVal left As String)
        _GANGE2_R = right
        _GANGE2_L = left
    End Sub
    Public Function Get_GRV2ON() As String
        Return "_GRV2ON=" & _GRV2ON_R & ";" & _GRV2ON_L & vbCrLf
    End Function
    Public Sub Set_GRV2SEL(ByVal right As String, ByVal left As String)
        _GRV2SEL_R = right
        _GRV2SEL_L = left
    End Sub
    Public Function Get_GRV2SEL() As String
        Return "_GRV2SEL=" & _GRV2SEL_R & ";" & _GRV2SEL_L & vbCrLf
    End Function
    Public Sub Set_RMONT(ByVal right As String, ByVal left As String)
        _RMONT_R = right
        _RMONT_L = left
    End Sub
    Public Function Get_RMONT() As String
        Return "_RMONT=" & _RMONT_R & ";" & _RMONT_L & vbCrLf
    End Function


    Public Function GetOMA_EdgerString() As String
        Dim OMAString As String = ""
        OMAString &= GetACCN()
        OMAString &= GetRXNM()
        OMAString &= GetANS()
        OMAString &= GetJOB()
        OMAString &= GetSTATUS()
        OMAString &= GetDO()
        OMAString &= GetLMATTYPE()
        OMAString &= GetLMATID()
        OMAString &= GetFTYP()
        OMAString &= GetETYP()
        OMAString &= Get_ETYP2()
        OMAString &= GetBEVP()
        OMAString &= GetBEVM()
        OMAString &= Get_CKBEV()
        OMAString &= GetFRNT()
        'OMAString &= Get_FRNT()
        OMAString &= GetPOLISH()
        'OMAString &= Get_POLISH2()
        OMAString &= GetBSIZ()
        OMAString &= GetPINB()
        OMAString &= GetFPINB()
        OMAString &= Get_PINONOF()
        OMAString &= Get_FPINONF()
        OMAString &= GetCTHICK()
        OMAString &= GetTHKP()
        'OMAString &= GetHBOX()
        'OMAString &= GetVBOX()
        'OMAString &= Get_IDSEQ()
        OMAString &= Get_VARINC()
        'OMAString &= Get_VVINCON()
        OMAString &= Get_VANGLE()
        'OMAString &= Get_VANGLON()
        OMAString &= Get_LCOAT()
        'OMAString &= Get_CHKRAD()
        'OMAString &= Get_TBASE()
        'OMAString &= Get_CHRFRAM()
        'OMAString &= Get_TBASEFR()
        OMAString &= Get_AUTOBFR()
        'OMAString &= Get_CKDOCUR()
        'OMAString &= Get_NDOCUR()
        'OMAString &= Get_CKSEQMAN()
        'OMAString &= Get_COPYDXSX()
        'OMAString &= Get_CURVELV()
        'OMAString &= Get_VDEPTON()
        OMAString &= GetGDEPTH()
        OMAString &= GetGWIDTH()
        'OMAString &= GET_GANGS
        'OMAString &= GET_GANGE
        'OMAString &= GET_GDEPTH2
        'OMAString &= GET_GWIDTH2
        'OMAString &= GET_GANGS2
        'OMAString &= GET_GANGE2
        'OMAString &= GET_GRV2ON
        'OMAString &= GET_GRV2SEL


        'OMAString &= Get_LPRESS()
        OMAString &= Get_CIRCON()
        OMAString &= GetCIRC()
        'OMAString &= Get_FBFCANG()
        OMAString &= GetFRAM()
        OMAString &= GetFCRV()

        'OMAString &= Get_RMONT()

        '        OMAString &= GetDRILL()
        '       OMAString &= GetDRILLE()
        '      OMAString &= Get_DRILLREF()
        '     OMAString &= Get_DRILLLNK()

        Select Case Me.DO_Command
            Case "B"
                OMAString &= GetTRCFMT(TRACESide.Right)
                OMAString &= GetTRCFMT(TRACESide.Left)
            Case "R"
                OMAString &= GetTRCFMT(TRACESide.Right)
            Case "L"
                OMAString &= GetTRCFMT(TRACESide.Left)
        End Select
        'OMAString &= "TBASE=0.75;0.75" + vbCrLf
        Return OMAString
    End Function
    Public Function GetOMAString() As String
        Dim OMAString As String = ""
        OMAString &= GetACCN()
        OMAString &= GetRXNM()
        OMAString &= GetSPH()
        OMAString &= GetCYL()
        OMAString &= GetAX()
        OMAString &= GetIPD()
        OMAString &= GetNPD()
        OMAString &= GetADD()
        OMAString &= GetSEGHT()
        OMAString &= GetOCHT()
        OMAString &= GetPRVA()
        OMAString &= GetPRVM()
        OMAString &= GetHBOX()
        OMAString &= GetVBOX()
        OMAString &= GetDBL()
        OMAString &= GetCIRC()
        OMAString &= GetFMAT()
        OMAString &= GetZTILT()
        OMAString &= GetPIND()
        OMAString &= GetLIND()
        OMAString &= GetLMATID()
        OMAString &= GetLMATNAME()
        OMAString &= GetLMATTYPE()
        OMAString &= GetLMFR()
        OMAString &= GetLMATNAME()
        OMAString &= GetLNAM()
        OMAString &= GetLTYP()
        OMAString &= GetCOLR()
        OMAString &= GetLSIZ()
        OMAString &= GetTINT()
        OMAString &= GetACOAT()
        OMAString &= GetDIA()
        OMAString &= GetMBASE()
        OMAString &= GetFRNT()
        OMAString &= GetBACK()
        OMAString &= GetSWIDTH()
        OMAString &= GetSDEPTH()
        OMAString &= GetOPC()
        OMAString &= GetSBSGUP()
        OMAString &= GetSBSGIN()
        OMAString &= GetSBBCUP()
        OMAString &= GetSBBCIN()
        OMAString &= GetGAX()
        OMAString &= GetBCOCIN()
        OMAString &= GetBCOCUP()
        OMAString &= GetBCSGIN()
        OMAString &= GetBCSGUP()
        OMAString &= GetBCTHK()
        OMAString &= GetBETHK()
        OMAString &= GetBPRVA()
        OMAString &= GetBPRVM()
        OMAString &= GetRNGD()
        OMAString &= GetRNGH()
        OMAString &= GetBLKB()
        OMAString &= GetBLKD()
        OMAString &= GetBLKTYP()
        OMAString &= GetKPRVA()
        OMAString &= GetKPRVM()
        OMAString &= GetTIND()
        OMAString &= GetSBFCUP()
        OMAString &= GetSBFCIN()
        OMAString &= GetGBASE()
        OMAString &= GetGCROS()
        OMAString &= GetGBASEX()
        OMAString &= GetGCROSX()
        OMAString &= GetGTHK()
        OMAString &= GetFINCMP()
        OMAString &= GetTHKCMP()
        OMAString &= GetBLKCMP()
        OMAString &= GetRNGCMP()
        OMAString &= GetSAGBD()
        OMAString &= GetSAGCD()
        OMAString &= GetSAGRD()
        OMAString &= GetGPRVA()
        OMAString &= GetGPRVM()
        OMAString &= GetCPRVA()
        OMAString &= GetCPRVM()
        OMAString &= GetIFRNT()
        OMAString &= GetOTHK()
        OMAString &= GetSBOCUP()
        OMAString &= GetSBOCIN()
        OMAString &= GetRPRVA()
        OMAString &= GetRPRVM()
        OMAString &= GetCRIB()
        OMAString &= GetAVAL()
        OMAString &= GetSVAL()
        OMAString &= GetELLH()
        OMAString &= GetFLATA()
        OMAString &= GetFLATB()
        OMAString &= GetPREEDGE()
        OMAString &= GetPADTHK()
        OMAString &= GetLAPBAS()
        OMAString &= GetLAPCRS()
        OMAString &= GetLAPBASX()
        OMAString &= GetLAPCRSX()
        OMAString &= GetLAPM()
        OMAString &= GetFBFCIN()
        OMAString &= GetFBFCUP()
        OMAString &= GetFBOCIN()
        OMAString &= GetFBOCUP()
        OMAString &= GetFBSGIN()
        OMAString &= GetFBSGUP()
        OMAString &= GetFCOCIN()
        OMAString &= GetFCOCUP()
        OMAString &= GetFCSGIN()
        OMAString &= GetFCSGUP()
        OMAString &= GetSGOCIN()
        OMAString &= GetSGOCUP()
        OMAString &= GetCTHICK()
        OMAString &= GetTHKA()
        OMAString &= GetTHKP()
        OMAString &= GetTHNA()
        OMAString &= GetTHNP()
        OMAString &= GetMCIRC()
        OMAString &= GetSLBP()
        If TRCFMT_R.Length > 0 Then
            OMAString &= GetTRCFMT(TRACESide.Right)
            OMAString &= GetZFMT(TRACESide.Right)
        End If
        If TRCFMT_L.Length > 0 Then
            OMAString &= GetTRCFMT(TRACESide.Left)
            OMAString &= GetZFMT(TRACESide.Left)
        End If
        OMAString &= GetETYP()
        OMAString &= GetFTYP()
        OMAString &= GetFPINB()
        OMAString &= GetPINB()
        OMAString &= GetGDEPTH()
        OMAString &= GetGWIDTH()
        OMAString &= GetBEVP()
        OMAString &= GetBEVC()
        OMAString &= GetPOLISH()
        OMAString &= GetCLAMP()
        OMAString &= GetDRILL()
        OMAString &= GetDRILLE()
        OMAString &= GetANS()


        Return OMAString
    End Function
    Public Sub WriteFile(ByVal Location As String)
        File.WriteAllText(Location, GetOMA_EdgerString())
    End Sub
End Class
