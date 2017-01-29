DROP TABLE IF EXISTS Meetings CASCADE;
DROP TABLE IF EXISTS Meeting_Times CASCADE;
DROP TABLE IF EXISTS Users CASCADE;
DROP TABLE IF EXISTS Messages CASCADE;
DROP TABLE IF EXISTS Attendees CASCADE;
DROP TABLE IF EXISTS Invitations CASCADE;

CREATE TABLE Users (
    user_id text
    ,display_name text
    ,email text
    ,PRIMARY KEY (user_id)
);

CREATE TABLE Meetings(
    meeting_id text
    ,creator_id text
    ,name text
    ,description text
    ,create_time timestamp
    ,start_fence timestamp
    ,end_fence timestamp
    ,day_start time
    ,day_end time
    ,length interval
    ,longitude real
    ,latitude real 
    ,location_description text
    ,PRIMARY KEY(meeting_id)
    ,FOREIGN KEY (creator_id) REFERENCES Users ON DELETE CASCADE
);

CREATE TABLE Meeting_Times (
    meeting_time_id text
    ,start_time timestamp
    ,meeting_id text
    ,PRIMARY KEY(start_time, meeting_id)
    ,FOREIGN KEY (meeting_id) REFERENCES Meetings ON DELETE CASCADE
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

CREATE TABLE Invitations (
    invitation_id text
    ,meeting_id text
    ,user_id text
    ,PRIMARY KEY (invitation_id)
    ,FOREIGN KEY (meeting_id) REFERENCES Meetings
);

CREATE TABLE Events (
    event_id text
    ,meeting_id text
    ,user_id text
    ,name text
    ,start timestamp
    ,length interval
    ,PRIMARY KEY (event_id)
    ,FOREIGN KEY (meeting_id) REFERENCES Meetings
    ,FOREIGN KEY (user_id) REFERENCES Users
);
