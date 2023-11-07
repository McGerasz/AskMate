--Should run them separetly

CREATE TABLE public.answers
(
    id integer NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 ),
    message text,
    question_id integer,
    submission_time date,
    PRIMARY KEY (id)
);

ALTER TABLE IF EXISTS public.answers
    ADD CONSTRAINT questions_fkey FOREIGN KEY (question_id)
    REFERENCES public.questions (id) MATCH SIMPLE
    ON UPDATE NO ACTION
    ON DELETE NO ACTION
    NOT VALID;


CREATE OR REPLACE FUNCTION check_question_id()
RETURNS TRIGGER AS $$
BEGIN
    IF NEW.question_id IS NOT NULL AND 
       NOT EXISTS (SELECT 1 FROM questions WHERE id = NEW.question_id) THEN
        RAISE EXCEPTION 'question_id does not reference an existing question';
    ELSE
        -- Only set the id explicitly if the question_id is valid
        NEW.id = COALESCE((SELECT MAX(id) FROM answers), 0) + 1;
    END IF;
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER check_question_id
BEFORE INSERT ON answers
FOR EACH ROW
EXECUTE FUNCTION check_question_id();


/* I KEEP THIS FOR BACKUP PLEASE IGNORE THIS
CREATE OR REPLACE FUNCTION check_question_id()
RETURNS TRIGGER AS $$
BEGIN
    IF NEW.question_id IS NOT NULL AND 
       NOT EXISTS (SELECT 1 FROM questions WHERE id = NEW.question_id) THEN
        RAISE EXCEPTION 'question_id does not reference an existing question';
    END IF;
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER check_question_id
BEFORE INSERT OR UPDATE ON answers
FOR EACH ROW
EXECUTE FUNCTION check_question_id();*/
