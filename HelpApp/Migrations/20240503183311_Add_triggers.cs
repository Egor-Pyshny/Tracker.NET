using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelpApp.Migrations
{
    /// <inheritdoc />
    public partial class Add_triggers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DELIMITER //

                BEFORE INSERT ON passport
                FOR EACH ROW
                BEGIN
                    IF LENGTH(NEW.id) != 16 THEN
                        SIGNAL SQLSTATE '45000'
                        SET MESSAGE_TEXT = 'wrong length';
                    END IF;
                END//

                BEFORE INSERT ON general_blood_analysis
                FOR EACH ROW
                BEGIN
                    IF EXISTS (SELECT 1 FROM general_blood_analysis WHERE file_path = NEW.file_path) THEN
                        SIGNAL SQLSTATE '45000'
                        SET MESSAGE_TEXT = 'wrong path';
                    END IF;
                END//

                BEFORE INSERT ON hospitals
                FOR EACH ROW
                BEGIN
                    IF EXISTS (SELECT 1 FROM hospitals WHERE name = NEW.name) THEN
                        SIGNAL SQLSTATE '45000'
                        SET MESSAGE_TEXT = 'wrong name';
                    END IF;
                END//

                BEFORE INSERT ON passport
                FOR EACH ROW
                BEGIN
                    IF EXISTS (SELECT 1 FROM passport WHERE second_name = NEW.second_name) THEN
                        SIGNAL SQLSTATE '45000'
                        SET MESSAGE_TEXT = 'wrong name';
                    END IF;
                END//

                BEFORE INSERT ON diagnosis
                FOR EACH ROW
                BEGIN
                    IF EXISTS (SELECT 1 FROM diagnosis WHERE name = NEW.name) THEN
                        SIGNAL SQLSTATE '45000'
                        SET MESSAGE_TEXT = 'wrong name';
                    END IF;
                END//

                BEFORE INSERT ON general_urine_analysis
                FOR EACH ROW
                BEGIN
                    IF EXISTS (SELECT 1 FROM general_urine_analysis WHERE file_path = NEW.file_path) THEN
                        SIGNAL SQLSTATE '45000'
                        SET MESSAGE_TEXT = 'wrong path';
                    END IF;
                END//

                BEFORE INSERT ON xray_results
                FOR EACH ROW
                BEGIN
                    IF EXISTS (SELECT 1 FROM xray_results WHERE file_path = NEW.file_path) THEN
                        SIGNAL SQLSTATE '45000'
                        SET MESSAGE_TEXT = 'wrong path';
                    END IF;
                END//

                CREATE TRIGGER check_recommendation_exists
                BEFORE INSERT ON doctors_recommendations
                FOR EACH ROW
                BEGIN
                    DECLARE doctor_count INT;
                    DECLARE patient_count INT;

                    SELECT COUNT(*) INTO doctor_count
                    FROM doctors
                    WHERE id = NEW.doctor_id;

                    SELECT COUNT(*) INTO patient_count
                    FROM patients
                    WHERE id = NEW.patient_id;

                    IF doctor_count = 0 OR patient_count = 0 THEN
                        SIGNAL SQLSTATE '45000'
                        SET MESSAGE_TEXT = 'Doctor or patient does not exist';
                    END IF;
                END//

                CREATE TRIGGER check_surgery_graphic_unique
                BEFORE INSERT ON doctorssurgeries_graphic
                FOR EACH ROW
                BEGIN
                    DECLARE existing_count INT;

                    SELECT COUNT(*) INTO existing_count
                    FROM doctorssurgeries_graphic
                    WHERE doctor_id = NEW.doctor_id AND date = NEW.date;

                    SELECT COUNT(*) INTO existing_count
                    FROM appointment_records
                    WHERE doctor_id = NEW.doctor_id AND date = NEW.date;

                    IF existing_count > 0 THEN
                        SIGNAL SQLSTATE '45000'
                        SET MESSAGE_TEXT = 'Surgery graphic already exists for this doctor and date';
                    END IF;
                END//

                CREATE TRIGGER check_appointment_graphic_unique
                BEFORE INSERT ON appointment_records
                FOR EACH ROW
                BEGIN
                    DECLARE existing_count INT;

                    SELECT COUNT(*) INTO existing_count
                    FROM doctorssurgeries_graphic
                    WHERE doctor_id = NEW.doctor_id AND date = NEW.date;

                    SELECT COUNT(*) INTO existing_count
                    FROM appointment_records
                    WHERE doctor_id = NEW.doctor_id AND date = NEW.date;

                    SELECT COUNT(*) INTO existing_count
                    FROM doctorssurgeries_graphic
                    WHERE patient_id = NEW.patient_id AND date = NEW.date;

                    SELECT COUNT(*) INTO existing_count
                    FROM appointment_records
                    WHERE doctor_id = NEW.doctor_id AND date = NEW.date OR patient_id = NEW.patient_id AND date = NEW.date;

                    IF existing_count > 0 THEN
                        SIGNAL SQLSTATE '45000'
                        SET MESSAGE_TEXT = 'graphic already exists for this doctor and date';
                    END IF;
                END//

                CREATE PROCEDURE GetPatientDetails(IN patientId INT)
                BEGIN
                    SELECT 
                        p.id,
                        ps.first_name,
                        ps.second_name,
                        ps.third_name,
                        ps.birthday,
                        ps.gender,
                        ps.birthday,
                        a.country,
                        a.city,
                        a.street,
                        a.building,
                        a.appartment,
                        p.email,
                        d.first_name,
                        d.second_name,
                        d.third_name,
                        GROUP_CONCAT(di.name SEPARATOR ' ')
                    FROM Patients p
                    JOIN Passport ps ON p.passportid = ps.id
                    JOIN Doctors d ON p.doctorid = d.id
                    JOIN address a ON ps.addressid = a.id
                    JOIN diagnosispatients dp ON p.id = dp.patientsid
                    JOIN diagnosis di ON dp.diagnosesid = di.id
                    WHERE p.id = patientId;
                END//

                CREATE PROCEDURE GetPatientReports(IN patientId INT)
                BEGIN
                    SELECT 
                        p.id,
                        gu.file_path AS UrineResult,
                        gb.file_path AS BloodResult,
                        x.file_path AS XRayResult,
                    FROM Patients p
                    JOIN general_urine_analysis gu ON p.id = gu.patientid
                    JOIN general_blood_analysis gb ON p.id = gb.patientid
                    JOIN xray_results x ON p.id = x.patientid
                    WHERE p.id = patientId;
                END//

                CREATE PROCEDURE GetPatientSurgeries(IN patientId INT)
                BEGIN
                    SELECT 
                        p.id,
                        sg.date AS UrineResult,
                        sh.file_path AS BloodResult,
                        d.first_name,
                        d.second_name,
                        d.third_name,
                    FROM Patients p
                    JOIN surgeries_graphic sg ON p.id = sg.patientid
                    JOIN surgeries_history sh ON p.id = sh.patientid
                    JOIN doctorssurgeries_graphic dsg ON sg.id = dsg.surgeries_Graphicsid
                    JOIN doctors d ON doctorssurgeries_graphic.Doctorsid = d.id
                    WHERE p.id = patientId;
                END//

                DELIMITER ;
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
