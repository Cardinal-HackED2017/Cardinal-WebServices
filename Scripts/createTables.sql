DROP TABLE IF EXISTS Meetings CASCADE;
DROP TABLE IF EXISTS Meeting_Times CASCADE;
DROP TABLE IF EXISTS Users CASCADE;
DROP TABLE IF EXISTS Messages CASCADE;
DROP TABLE IF EXISTS Attendees CASCADE;

CREATE TABLE Meetings(
    meeting_id text
    ,name text
    ,create_time timestamp
    ,start_fence timestamp
    ,end_fence timestamp
    ,length interval
    ,longitude real
    ,latitude real 
    ,location_description text
    ,PRIMARY KEY(meeting_id)
);

CREATE TABLE Meeting_Times (
    meeting_time_id text
    ,start_time timestamp
    ,meeting_id text
    ,PRIMARY KEY(start_time, meeting_id)
    ,FOREIGN KEY (meeting_id) REFERENCES Meetings ON DELETE CASCADE
);

CREATE TABLE Users (
    user_id text
    ,display_name text
    ,email text
    ,PRIMARY KEY (user_id)
);

CREATE TABLE Messages (
    message_id text
    ,meeting_id text
    ,user_id text
    ,content text
    ,created_time timestamp
    ,PRIMARY KEY(message_id, meeting_id, user_id)
    ,FOREIGN KEY (meeting_id) REFERENCES Meetings
    ,FOREIGN KEY (user_id) REFERENCES Users
);

CREATE TABLE Attendees (
    meeting_id text
    ,user_id text
    ,PRIMARY KEY (meeting_id, user_id)
    ,FOREIGN KEY (meeting_id) REFERENCES Meetings
    ,FOREIGN KEY (user_id) REFERENCES Users
);